using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DisposableDemo
{
    public class ResourceUser: IDisposable
    {
        private FileStream stream;
        // large array just to take memory
        private byte[] bytes = new byte[10_000_000];

        public ResourceUser()
        {
            Console.WriteLine("Opening file stream ");
            stream = File.Open(@"D:\grupna.txt", FileMode.OpenOrCreate);
        }

        //... some methods

        public void CloseFile()
        {
            Console.WriteLine("Closing file stream");
            stream.Close();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CloseFile();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                bytes = null;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~ResourceUser()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
