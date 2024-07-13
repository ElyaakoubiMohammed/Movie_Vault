using System;

namespace ControlInventoryManagment.Exceptions;
    public class NotSavedException : Exception
    {
        public NotSavedException() { }

        public NotSavedException(string message) : base(message) { }

        public NotSavedException(string message, Exception innerException) : base(message, innerException) { }
    }

