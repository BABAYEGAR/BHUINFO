using System;
using System.IO;
using System.Web;
using BhuInfo.Data.Service.Enums;

/// <summary>
///     Summary description for FileUploader
/// </summary>
public class FileUploader
{
    /// <summary>
    ///     Uploads documents to the upload folder
    /// </summary>
    /// <param name="fileUpload">The FileUpload object that contains the file to be uploaded</param>
    /// <param name="uploadType">The uoload file type</param>
    /// <returns>The saved file name and extension</returns>
    public string UploadFile(HttpPostedFileBase fileUpload, UploadType uploadType)
    {
        var filename = DateTime.Now.ToFileTime().ToString();

        if (fileUpload != null)
        {
            var fileInfo = new FileInfo(fileUpload.FileName);
            if ((fileInfo.Extension.ToLower() == ".jpg") || (fileInfo.Extension.ToLower() == ".jpeg")
                || (fileInfo.Extension.ToLower() == ".png"))
                try
                {
                    var fileExtension = fileInfo.Extension;

                    filename = DateTime.Now.ToFileTime() + fileExtension;

                    //check if upload folder is available. Else create it
                    var uploadFolderPath =
                        HttpContext.Current.Server.MapPath("~/BhuInfo.Data.Service/UploadedFiles/" + uploadType);

                    //check to see if the directory exists else, create directory
                    if (!Directory.Exists(uploadFolderPath))
                        Directory.CreateDirectory(uploadFolderPath);

                    //save image
                    var imagePath = uploadFolderPath + "/" + filename;
                    fileUpload.SaveAs(imagePath);
                }
                catch (Exception ex)
                {
                }
        }

        return filename;
    }


    public bool ThumbnailCallback()
    {
        return false;
    }
}