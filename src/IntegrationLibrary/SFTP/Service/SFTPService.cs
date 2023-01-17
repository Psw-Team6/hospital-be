using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.SFTP.Service
{
    public class SFTPService : ISFTPService
    {
        public byte[] DownloadFileFromRebexServer(string fileName)
        {
            byte[] fileData = new byte[16 * 1024];

            using (SftpClient client = new SftpClient("127.0.0.1", 2222, "tester", "password"))
            {
                client.Connect();

                using (var stream = new MemoryStream())
                {
                    client.DownloadFile(@"/public/" + fileName, stream);

                    fileData = stream.ToArray();

                }

                client.Disconnect();
            }

            return fileData;
        }

        public void ShowAllFilesFromRebexServer()
        {
            using (SftpClient client = new SftpClient("127.0.0.1", 2222, "tester", "password"))
            {
                client.Connect();

                var paths = client.ListDirectory(@"/public/");
                foreach (var path in paths) 
                {
                    Console.WriteLine(path.IsDirectory?"Directory: " + path.FullName : "File: " + path.FullName);
                }

                client.Disconnect();
            }
        }

        public void UploadFileToRebexServer(byte[] file, String fileName)
        {
            using (SftpClient client = new SftpClient("127.0.0.1", 2222, "tester", "password")) 
            {
                client.Connect();

                using (Stream stream = new MemoryStream(file))
                {
                    client.UploadFile(stream, @"/public/" + fileName);
                }

                /*using (Stream stream = File.OpenRead(@"C:\Users\X\Desktop\Fakultet\PostgreSQL.txt")) 
                {
                    client.UploadFile(stream, @"\public\" + Path.GetFileName(@"C:\Users\X\Desktop\Fakultet\PostgreSQL.txt"));    
                }*/

                client.Disconnect();
            }
        }
    }
}
