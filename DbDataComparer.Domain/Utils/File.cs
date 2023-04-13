using System;
using System.IO;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Utils
{
    /// <summary>
    /// General routines regarding Files
    /// </summary>
    public static class File
    {
        /// <summary>
        /// Move file to specified destination
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        public static async Task MoveAsync(string sourceFileName, string destFileName)
        {
            const int BUFFER_SIZE = 8192;       // Assuming Move is across network and network is using Jumbo Frames

            // Let's mimic the File.Move Exceptions first.
            // The order is imperative.
            if (sourceFileName == null)
                throw new ArgumentNullException($"{nameof(sourceFileName)} is null");

            if (destFileName == null)
                throw new ArgumentNullException($"{nameof(destFileName)} is null");

            if (String.IsNullOrWhiteSpace(sourceFileName))
                throw new ArgumentException($"{nameof(sourceFileName)} is a zero-length string or contains white space only.");

            if (String.IsNullOrWhiteSpace(destFileName))
                throw new ArgumentException($"{nameof(destFileName)} is a zero-length string or contains white space only.");

            if (!System.IO.File.Exists(sourceFileName))
                throw new FileNotFoundException($"{sourceFileName} was not found");

            if (System.IO.File.Exists(destFileName))
                throw new IOException($"{destFileName} already exists.");


            // Made it this far, so let's get busy
            // File.Copy does not have Async capabilities.  Not sure how big the files can get.  So use the following routine to minimize resources
            using (FileStream sourceFS = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read))
            {
                using (FileStream targetFS = new FileStream(destFileName, FileMode.CreateNew, FileAccess.Write))
                {
                    await sourceFS.CopyToAsync(targetFS, BUFFER_SIZE);
                }
            }

            System.IO.File.Delete(sourceFileName);
        }
    }
}
