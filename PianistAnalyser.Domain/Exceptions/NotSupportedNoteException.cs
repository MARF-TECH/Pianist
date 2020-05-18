using System;

namespace PianistAnalyser.Domain.Exceptions
{

    public partial class NotSupportedNoteException : Exception
    {
        public NotSupportedNoteException() : base("Error: Wrong or Unsupported Note")
        {
        }
    }
}