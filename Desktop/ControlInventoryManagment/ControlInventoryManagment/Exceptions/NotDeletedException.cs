namespace ControlInventoryManagment.Exceptions;

    public class NotDeletedException : Exception
    {
        public NotDeletedException() : base() { }

        public NotDeletedException(string message) : base(message) { }

        public NotDeletedException(string message, Exception innerException)
            : base(message, innerException) { }
    }

