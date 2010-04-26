using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Principal;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;
using System.Security;
using System.Collections.Generic;
using System.Collections.Specialized;
using iTextSharp.text.pdf;
using iTextSharp.text;


/// <summary>
/// Summary description for RecruitmentUploadService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RecruitmentUploadService : System.Web.Services.WebService
{
    private const string STR_SALT = "Recruitment";
    private const string STR_LetterOfRec = "LetterOfRec";
    private const string STR_Publication = "Publication";

    public string FilePath
    {
        get
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings["RecruitmentFilePath"];
        }
    }

    public IDaoFactory daoFactory
    {
        get { return new NHibernateDaoFactory(); }
    }

    public RecruitmentUploadService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Saves the file given by the byte[] into the fileSystem and enters a corresponding file record into
    /// the database
    /// </summary>
    /// <param name="type">FileType enum can be References, Publications, or Other Files</param>
    /// <param name="applicationID">The applicationID</param>
    /// <param name="id">The ReferenceID or FileTypeID (depending on fileType)</param>
    /// <param name="fileContent">The byte[] of the file content</param>
    [WebMethod]
    public bool SaveFile(fileTypes type, string applicationID, string id, byte[] fileContent, string SID, string hash)
    {
        EnsureValidHash(SID, hash);

        User user = NullSaveGetUser(SID);

        int appID = 0;

        if (int.TryParse(applicationID, out appID) == false)
            return false;
        
        if (type == fileTypes.References)
        {
            int referenceID = 0;

            if (int.TryParse(id, out referenceID) == false)
                return false;

            return UploadReferences(referenceID, fileContent);
        }
        else if (type == fileTypes.Publications)
        {
            return UploadPublications(appID, fileContent);
        }
        else if (type == fileTypes.Other)
        {
            int fileTypeID = 0;

            if (int.TryParse(id, out fileTypeID) == false)
                return false;

            return UploadFiles(appID, fileTypeID, fileContent);
        }
        else
        {
            return false;
        }
    }

    [WebMethod]
    public KeyPairSerializable[] GetPositions(string SID, string hash)
    {
        List<KeyPairSerializable> positions = new List<KeyPairSerializable>();
        
        EnsureValidHash(SID, hash);

        User user = NullSaveGetUser(SID);

        foreach (Position p in daoFactory.GetPositionDao().GetAll())
        {
            if ( p.ApplicationCount > 0 )
                positions.Add(new KeyPairSerializable(p.TitleAndApplicationCount, p.ID.ToString()));
        }

        return positions.ToArray();
    }

    [WebMethod]
    public KeyPairSerializable[] GetApplications(string positionID, string SID, string hash)
    {
        List<KeyPairSerializable> applications = new List<KeyPairSerializable>();

        EnsureValidHash(SID, hash);

        User user = NullSaveGetUser(SID);

        int posID = 0;

        if ( !int.TryParse(positionID, out posID) )
            return applications.ToArray();

        Position currentPosition = daoFactory.GetPositionDao().GetById(posID, false);

        foreach (Application app in daoFactory.GetApplicationDao().GetApplicationsByPosition(currentPosition))
        {
            string name = string.IsNullOrEmpty(app.AssociatedProfile.FullName.Trim()) ? app.Email : app.AssociatedProfile.FullName;

            applications.Add(new KeyPairSerializable(name, app.ID.ToString()));
        }

        return applications.ToArray();
    }

    [WebMethod]
    public KeyPairSerializable[] GetReferences(string applicationID, string SID, string hash)
    {
        List<KeyPairSerializable> references = new List<KeyPairSerializable>();

        EnsureValidHash(SID, hash);

        User user = NullSaveGetUser(SID);

        int appID = 0;

        if (!int.TryParse(applicationID, out appID))
            return references.ToArray();

        Application currentApplication = daoFactory.GetApplicationDao().GetById(appID, false);

        foreach (Reference reference in currentApplication.References )
        {
            references.Add(new KeyPairSerializable(reference.FullName, reference.ID.ToString()));
        }

        return references.ToArray();
    }

    [WebMethod]
    public KeyPairSerializable[] GetFileTypes(string SID, string hash)
    {
        List<KeyPairSerializable> fileTypes = new List<KeyPairSerializable>();

        EnsureValidHash(SID, hash);

        User user = NullSaveGetUser(SID);

        foreach (FileType type in daoFactory.GetFileTypeDao().GetAllByApplicationFileType(true, "FileTypeName", true))
        {
            fileTypes.Add(new KeyPairSerializable(type.FileTypeName, type.ID.ToString()));
        }

        return fileTypes.ToArray();
    }

    private string GetHash(string SID)
    {
        string secureString = SID + DateTime.Now.ToShortDateString() + STR_SALT;
        byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(secureString);

        System.Security.Cryptography.SHA256Managed hash = new System.Security.Cryptography.SHA256Managed();

        return Convert.ToBase64String(hash.ComputeHash(b));
    }

    private User NullSaveGetUser(string SID)
    {
        User user = daoFactory.GetUserDao().GetUserBySID(SID);

        if (user == null)
            throw new System.Security.Authentication.InvalidCredentialException(string.Format("User with SID: {0} not found", SID));
        else
            return user;
    }

    private void EnsureValidHash(string SID, string hash)
    {
        if (IsHashValid(SID, hash) == false)
            throw new SecurityException(string.Format("User with SID: {0} did not supply a valid hash", SID));
    }

    private bool IsHashValid(string SID, string hash)
    {
        if (hash == this.GetHash(SID))
            return true;
        else
            return false;
    }

    #region UploadMethods

    private bool UploadReferences(int referenceID, byte[] uploadedFile)
    {
        FileType referenceFileType = daoFactory.GetFileTypeDao().GetFileTypeByName(STR_LetterOfRec);

        Reference selectedReference = daoFactory.GetReferenceDao().GetById(referenceID, false);

        //If there is already a reference file, we need to delete it
        if (selectedReference.ReferenceFile != null)
        {
            using (new NHibernateTransaction())
            {
                int fileID = selectedReference.ReferenceFile.ID;

                daoFactory.GetFileDao().Delete(selectedReference.ReferenceFile);
                selectedReference.ReferenceFile = null;

                //Delete the file from the file system
                System.IO.FileInfo fileToDelete = new System.IO.FileInfo(FilePath + fileID.ToString());
                fileToDelete.Delete();

                daoFactory.GetReferenceDao().SaveOrUpdate(selectedReference);
            }
        }

        File file = new File();

        file.FileName = selectedReference.FullName + ".pdf";
        file.FileType = referenceFileType;

        using (new NHibernateTransaction())
        {
            file = daoFactory.GetFileDao().Save(file);
        }

        if (ValidateBO<File>.isValid(file))
        {
            SaveReferenceWithWatermark(uploadedFile, file.ID.ToString());
                        
            selectedReference.ReferenceFile = file;

            using (new NHibernateTransaction())
            {
                daoFactory.GetReferenceDao().SaveOrUpdate(selectedReference);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private bool UploadFiles(int applicationID, int fileTypeID, byte[] uploadedFile)
    {
        FileType selectedFileType = daoFactory.GetFileTypeDao().GetById(fileTypeID, false);
        Application selectedApplication = daoFactory.GetApplicationDao().GetById(applicationID, false);

        //For all fileTypes except for Publications we should remove existing files
        if (selectedFileType.FileTypeName != STR_Publication && selectedFileType.FileTypeName != STR_LetterOfRec)
            RemoveAllFilesOfType(selectedApplication, selectedFileType.FileTypeName);

        File file = new File();

        file.FileName = selectedFileType.FileTypeName + ".pdf";
        file.FileType = selectedFileType;

        using (new NHibernateTransaction())
        {
            file = daoFactory.GetFileDao().Save(file);
        }

        if (ValidateBO<File>.isValid(file))
        {
            System.IO.File.WriteAllBytes(FilePath + file.ID.ToString(), uploadedFile);
                        
            selectedApplication.Files.Add(file);

            using (new NHibernateTransaction())
            {
                daoFactory.GetApplicationDao().SaveOrUpdate(selectedApplication);
            }

            return true;
        }
        else
        {
            return false;
        }         
    }

    private bool UploadPublications(int applicationID, byte[] uploadedFile)
    {
        FileType publicationsFileType = daoFactory.GetFileTypeDao().GetFileTypeByName(STR_Publication);
        Application selectedApplication = daoFactory.GetApplicationDao().GetById(applicationID, false);

        File publication = new File();

        publication.FileName = publicationsFileType.FileTypeName + ".pdf";
        publication.FileType = publicationsFileType;

        using (new NHibernateTransaction())
        {
            publication = daoFactory.GetFileDao().Save(publication);
        }

        if (ValidateBO<File>.isValid(publication))
        {
            System.IO.File.WriteAllBytes(FilePath + publication.ID.ToString(), uploadedFile);

            selectedApplication.Files.Add(publication);

            using (new NHibernateTransaction())
            {
                daoFactory.GetApplicationDao().SaveOrUpdate(selectedApplication);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void SaveReferenceWithWatermark(byte[] uploadedFile, string fileName)
    {
        PdfReader reader = new PdfReader(uploadedFile);

        int n = reader.NumberOfPages;

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
    private void RemoveAllFilesOfType(Application selectedApplication, string fileTypeName)
    {
        List<File> existingFiles = GetFilesOfType(selectedApplication, fileTypeName);

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
    private List<File> GetFilesOfType(Application selectedApplication, string fileTypeName)
    {
        if (selectedApplication == null)
            return new List<File>();

        List<File> correctTypeFiles = new List<File>();

        foreach (File f in selectedApplication.Files)
        {
            if (f.FileType.FileTypeName == fileTypeName)
                correctTypeFiles.Add(f);
        }

        return correctTypeFiles;
    }
    #endregion
}

[Serializable]
public class KeyPairSerializable
{
    public string key = string.Empty;
    public string value = string.Empty;

    public KeyPairSerializable(string key, string value)
    {
        this.key = key;
        this.value = value;
    }

    public KeyPairSerializable()
    {

    }
}

[Serializable]
public enum fileTypes
{
    References,
    Publications,
    Other
}