using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAESDO.Recruitment.Core.Domain;
using System.Web.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace CAESDO.Recruitment.BLL
{
    public class FileBLL : GenericBLL<File, int>
    {
        public static string FilePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["RecruitmentFilePath"];
            }
        }

        /// <summary>
        /// Saves the content of the fileUpload control to the database and file system
        /// as the given type.  The file object is returned on success, else null.
        /// </summary>
        public static File SavePDF(FileUpload fileUpload, FileType fileType)
        {
            if (fileUpload.HasFile)
            {
                if (IsPostedFilePDF(fileUpload.PostedFile))
                {
                    File file = new File();

                    file.FileName = fileUpload.FileName;
                    file.FileType = fileType;

                    using (var ts = new TransactionScope())
                    {
                        bool saveSuccess = FileBLL.MakePersistent(file);

                        // Save is a success if the persistence worked and all validation rules are satisfied
                        if (saveSuccess)
                        {
                            fileUpload.SaveAs(FilePath + file.ID.ToString());

                            ts.CommitTransaction(); //On success we commit

                            return file;
                        }
                    }
                }
            }

            return null;
        }

        public static File SavePDFWithWatermark(FileUpload fileUpload, FileType fileType)
        {
            File file = new File();

            if (fileUpload.HasFile)
            {
                if (IsPostedFilePDF(fileUpload.PostedFile))
                {
                    file.FileName = fileUpload.FileName;
                    file.FileType = fileType;
                    
                    bool saveSuccess = FileBLL.MakePersistent(file);

                    // Save is a success if the persistence worked and all validation rules are satisfied
                    if (!saveSuccess)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            try
            {
                PdfReader reader = new PdfReader(fileUpload.FileContent);

                int n = reader.NumberOfPages;

                Document document = new Document(reader.GetPageSizeWithRotation(1));

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(FilePath + file.ID.ToString(), System.IO.FileMode.Create));

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
            catch
            {
                //If the 
            }

            return file;
        }

        public static void DeletePDF(File file)
        {
            //Delete the old file
            System.IO.FileInfo oldFile = new System.IO.FileInfo(FilePath + file.ID.ToString());
            oldFile.Delete();

            FileBLL.Remove(file);
        }

        public static List<File> GetByTypeAndApplication(Application application, string fileType)
        {
            List<File> correctTypeFiles = new List<File>();

            if (application == null)
                return new List<File>();
            
            foreach (File file in application.Files)
            {
                if (file.FileType.FileTypeName == fileType)
                    correctTypeFiles.Add(file);
            }

            return correctTypeFiles;
        }

        /// <summary>
        /// Transmits the indicated file to the user's browser.
        /// </summary>
        /// <param name="fileID">ID of the file, inside of the FilePath</param>
        /// <param name="friendlyName">The name of the returned file as seen by the user</param>
        /// <returns>true on success</returns>
        public static bool Transmit(int fileID, string friendlyName)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(FilePath + fileID.ToString());
            HttpResponse response = HttpContext.Current.Response;

            if (!file.Exists)
            {
                return false;
            }

            response.Clear();

            //Control the name that they see
            response.ContentType = "application/octet-stream";
            response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(friendlyName));
            response.AddHeader("Content-Length", file.Length.ToString());
            response.TransmitFile(file.FullName);
            response.End();

            return true;
        }

        /// <summary>
        /// Downloads all files associated with the current application
        /// </summary>
        /// <returns>True on success</returns>
        public static bool DownloadByApplication(Application application, bool includeReferences)
        {
            string fileName = application.AssociatedProfile.LastName + "-Application" + application.ID.ToString() + ".pdf";
            string destination = FilePath + fileName;
            
            if (application.Files.Count == 0)
                return false;

            var filesToDownload = new List<File>(application.Files);

            if (includeReferences)
            {
                var referenceFiles = from reference in application.References
                                     where reference.ReferenceFile != null
                                     select reference.ReferenceFile;

                filesToDownload.AddRange(referenceFiles);
            }

            HttpResponse response = HttpContext.Current.Response;

            try
            {
                int f = 0;

                PdfReader reader = new PdfReader(FilePath + filesToDownload[f].ID.ToString());

                int n = reader.NumberOfPages;

                //response.Write(string.Format("There are {0} pages in the original docuemnt", n));

                Document document = new Document(reader.GetPageSizeWithRotation(1));

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(destination, System.IO.FileMode.Create));

                document.Open();

                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;

                while (f < filesToDownload.Count)
                {
                    Chapter chapter = new Chapter(string.Empty, f + 1)
                                          {
                                              BookmarkTitle = filesToDownload[f].FileType.FileTypeName,
                                              NumberDepth = 0
                                          };


                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();

                        if (i == 1)
                            document.Add(chapter);

                        page = writer.GetImportedPage(reader, i);

                        rotation = reader.GetPageRotation(i);

                        if (rotation == 90 || rotation == 270)
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        else
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);

                        BaseFont bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        cb.BeginText();
                        cb.SetFontAndSize(bf, 12f);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, filesToDownload[f].FileType.FileTypeName + " -- " + filesToDownload[f].FileName, reader.GetPageSizeWithRotation(i).Width / 2, reader.GetPageSizeWithRotation(i).Height - 12f, 0);
                        //cb.ShowText(currentApplication.Files[f].FileType.FileTypeName);
                        cb.EndText();
                    }
                    f++;

                    if (f < filesToDownload.Count)
                    {
                        reader = new PdfReader(FilePath + filesToDownload[f].ID.ToString());

                        n = reader.NumberOfPages;
                    }
                }

                document.Close();
            }
            catch (Exception)
            {
                return false;
            }

            //If we got this far, no exceptions have been made
            System.IO.FileInfo file = new System.IO.FileInfo(destination);

            response.Clear();

            //Control the name that they see
            response.ContentType = "application/octet-stream";
            response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            response.AddHeader("Content-Length", file.Length.ToString());
            //response.TransmitFile(path + FileID.ToString());
            response.TransmitFile(file.FullName);

            response.End();

            //Now remove the temporary file
            file.Delete();

            return true;
        }

        public static List<File> Sort(List<File> fileList, SortDirection sortDirection)
        {
            fileList.Sort(new FileTypeComparer(sortDirection));

            return fileList;
        }

        /// <summary>
        /// Returns true if posted file has the PDF extension
        /// </summary>
        /// <returns></returns>
        public static bool IsPostedFilePDF(HttpPostedFile file)
        {
            string extension = System.IO.Path.GetExtension(file.FileName);

            return string.Equals(extension, ".pdf", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Comparer that uses the FileTypeName of a file for comparison
        /// </summary>
        public class FileTypeComparer : IComparer<File>
        {
            private SortDirection sortDirection;

            public SortDirection SortDirection
            {
                get { return this.sortDirection; }
                set { this.sortDirection = value; }
            }

            public FileTypeComparer(SortDirection sortDirection)
            {
                this.SortDirection = sortDirection;
            }

            #region IComparer<File> Members

            public int Compare(File x, File y)
            {
                if (sortDirection == SortDirection.Ascending)
                    return x.FileType.FileTypeName.CompareTo(y.FileType.FileTypeName);
                else
                    return y.FileType.FileTypeName.CompareTo(x.FileType.FileTypeName);
            }

            #endregion
        }
    }
}
