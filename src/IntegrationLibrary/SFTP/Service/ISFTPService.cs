using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.SFTP.Service
{
    public interface ISFTPService
    {
        public void UploadFileToRebexServer(byte[] file, String fileName);
        public byte[] DownloadFileFromRebexServer(String fileName);
        public void ShowAllFilesFromRebexServer();
    }
}
