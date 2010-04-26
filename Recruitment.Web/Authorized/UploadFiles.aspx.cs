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
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_UploadFiles : ApplicationPage
    {
        private const string STR_Applicationpdf = "application/pdf";
        private const string STR_Publication = "Publication";
        private const string STR_LetterOfRec = "LetterOfRec";
        private const string REFERENCE_VALUE = "0";

        public Application selectedApplication 
        {
            get {
                return daoFactory.GetApplicationDao().GetById(int.Parse(dlistApplications.SelectedValue), false);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void rlistUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mViewFileType.ActiveViewIndex = int.Parse(rlistUploadType.SelectedValue);
        }

        /// <summary>
        /// Uploads a file of the given type into the selected application
        /// </summary>
        protected void btnfileUpload_Click(object sender, EventArgs e)
        {
            if (rlistUploadType.SelectedValue == REFERENCE_VALUE)
            {
                UploadReferences();
            }
            else
            {
                UploadFiles();
            }
        }

        private void UploadReferences()
        {
            FileType referenceFileType = daoFactory.GetFileTypeDao().GetFileTypeByName(STR_LetterOfRec);
            Reference selectedReference = daoFactory.GetReferenceDao().GetById(int.Parse(dlistReferences.SelectedValue), false);

            //If there is already a reference file, we need to delete it
            if (selectedReference.ReferenceFile != null)
            {
                using (new NHibernateTransaction())
                {
                    int fileID = selectedReference.ReferenceFile.ID;

                    daoFactory.GetFileDao().Delete(selectedReference.ReferenceFile);
                    selectedReference.ReferenceFile = null;

                    //Delete the file from the file system
                    System.IO.FileInfo file = new System.IO.FileInfo(FilePath + fileID.ToString());
                    file.Delete();

                    daoFactory.GetReferenceDao().SaveOrUpdate(selectedReference);
                }

            }

            if (fileUpload.HasFile)
            {
                if (fileUpload.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File file = new File();

                    file.FileName = fileUpload.FileName;
                    file.FileType = referenceFileType;

                    using (new NHibernateTransaction())
                    {
                        file = daoFactory.GetFileDao().Save(file);
                    }

                    if (ValidateBO<File>.isValid(file))
                    {
                        SaveReferenceWithWatermark(fileUpload, file.ID.ToString());

                        //fileUpload.SaveAs(FilePath + file.ID.ToString());

                        selectedReference.ReferenceFile = file;

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetReferenceDao().SaveOrUpdate(selectedReference);
                        }

                        lblStatus.Text = "File Uploaded Successfully";
                    }
                    else
                    {
                        //TODO: Handle non-validating file
                    }
                }//TODO: Handle if content not PDF
            }
        }
        
        private void UploadFiles()
        {
            FileType selectedFileType = daoFactory.GetFileTypeDao().GetById(int.Parse(dlistFileTypes.SelectedValue), false);

            //For all fileTypes except for Publications we should remove existing files
            if (selectedFileType.FileTypeName != STR_Publication && selectedFileType.FileTypeName != STR_LetterOfRec)
                RemoveAllFilesOfType(selectedFileType.FileTypeName);

            if (fileUpload.HasFile)
            {
                if (fileUpload.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File file = new File();

                    file.FileName = fileUpload.FileName;
                    file.FileType = selectedFileType;

                    using (new NHibernateTransaction())
                    {
                        file = daoFactory.GetFileDao().Save(file);
                    }

                    if (ValidateBO<File>.isValid(file))
                    {
                        fileUpload.SaveAs(FilePath + file.ID.ToString());

                        selectedApplication.Files.Add(file);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(selectedApplication);
                        }

                        lblStatus.Text = "File Uploaded Successfully";
                    }
                    else
                    {
                        //TODO: Handle non-validating file
                    }
                }//TODO: Handle if content not PDF
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
        

        /// <summary>
        /// Removes all files of the given type from the current applicaiton.  This removes the files themselves,
        /// the file info entry and the application files link
        /// </summary>
        private void RemoveAllFilesOfType(string fileTypeName)
        {
            List<File> existingFiles = GetFilesOfType(fileTypeName);

            if (existingFiles.Count != 0)
            {
                using (new NHibernateTransaction())
                {
                    foreach (File existingFile in existingFiles)
                    {
                        selectedApplication.Files.Remove(existingFile);
                        daoFactory.GetFileDao().Delete(existingFile);

                        //Delete the file from the file system
                        System.IO.FileInfo file = new System.IO.FileInfo(FilePath + existingFile.ID.ToString());
                        file.Delete();
                    }

                    daoFactory.GetApplicationDao().SaveOrUpdate(selectedApplication);
                }
            }
        }

        /// <summary>
        /// Parses down the currentApplication files list to just contain files of the correct type
        /// </summary>
        /// <param name="fileTypeName">The name of the file type desired</param>
        /// <returns>Just the currentApplication files of the given type</returns>
        private List<File> GetFilesOfType(string fileTypeName)
        {
            List<File> correctTypeFiles = new List<File>();

            foreach (File f in selectedApplication.Files)
            {
                if (f.FileType.FileTypeName == fileTypeName)
                    correctTypeFiles.Add(f);
            }

            return correctTypeFiles;
        }
    }

}