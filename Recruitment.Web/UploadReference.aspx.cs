using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace CAESDO.Recruitment.Web
{
    public partial class UploadReference : ApplicationPage
    {
        private const string STR_LetterOfRec = "LetterOfRec";
        private const string STR_Applicationpdf = "application/pdf";

        public Reference currentReference
        {
            get
            {
                return daoFactory.GetReferenceDao().GetReferenceByUploadID(Request.QueryString["ID"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //make sure the current reference exists and doesn't have a file already
            if (currentReference == null || currentReference.ReferenceFile != null)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }

            if (!IsPostBack)
            {
                lblInfo.Text = "Reference for " + currentReference.AssociatedApplication.AssociatedProfile.FullName;
            }
        }

        protected void btnUploadReference_Click(object sender, EventArgs e)
        {
            this.UploadReferences();
        }

        private void UploadReferences()
        {
            FileType referenceFileType = daoFactory.GetFileTypeDao().GetFileTypeByName(STR_LetterOfRec);
                        
            if (fileUploadReference.HasFile)
            {
                if (fileUploadReference.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File file = new File();

                    file.FileName = fileUploadReference.FileName;
                    file.FileType = referenceFileType;

                    using (new NHibernateTransaction())
                    {
                        file = daoFactory.GetFileDao().Save(file);
                    }

                    if (ValidateBO<File>.isValid(file))
                    {
                        SaveReferenceWithWatermark(fileUploadReference, file.ID.ToString());

                        currentReference.ReferenceFile = file;

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetReferenceDao().SaveOrUpdate(currentReference);
                        }

                        Response.Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else
                    {
                        lblUploadStatus.Text = "There was an unexpected error uploading your file";
                    }
                }
                else
                {
                    lblUploadStatus.Text = "Please upload a file in PDF format";
                }
            }
        }

        private void SaveReferenceWithWatermark(FileUpload uploadedFile, string fileName)
        {
            PdfReader reader = new PdfReader(uploadedFile.FileContent);

            int n = reader.NumberOfPages;

            Response.Write(string.Format("There are {0} pages", n));

            Document document = new Document(reader.GetPageSizeWithRotation(1));

            PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(FilePath + fileName, System.IO.FileMode.Create));

            document.Open();

            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;
            int rotation;

            for (int i = 1; i <= n; i++)
            {
                document.SetPageSize(reader.GetPageSizeWithRotation(i));
                document.NewPage();

                page = writer.GetImportedPage(reader, i);

                rotation = reader.GetPageRotation(i);

                if (rotation == 90 || rotation == 270)
                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                else
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);

                BaseFont bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.BeginText();
                cb.SetFontAndSize(bf, 26f);
                cb.SetColorFill(Color.RED);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CONFIDENTIAL", reader.GetPageSizeWithRotation(i).Width / 2f, reader.GetPageSizeWithRotation(i).Height - 26f, 0);
                //cb.ShowText(currentApplication.Files[f].FileType.FileTypeName);
                cb.EndText();

            }

            document.Close();
        }
    } 
}
