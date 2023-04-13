using System;
using System.IO;

namespace DbDataComparer.Domain.Utils
{
    /// <summary>
    /// General routines regarding Streams
    /// </summary>
    public static class Stream
    {
        /// <summary>
        /// This will take a string data and convert to a stream
        /// </summary>
        /// <param name="data"></param>
        public static System.IO.MemoryStream ConvertToMemoryStream(string data)
        {
            MemoryStream ms = new MemoryStream();

            if (!String.IsNullOrWhiteSpace(data))
            {
                StreamWriter sw = new StreamWriter(ms);
                sw.Write(data);
                sw.Flush();

                Stream.ResetStreamPointer(ms);
            }

            return ms;
        }

        /// <summary>
        /// This will reposition the stream pointer back to the beginning
        /// </summary>
        /// <remarks>
        /// This first checks to make sure the stream has the capbilities to reset the pointer
        /// </remarks>
        /// <param name="data"></param>
        public static void ResetStreamPointer(System.IO.Stream stream)
        {
            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);
        }
    }
}
