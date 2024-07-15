
namespace ControlInventoryManagment.Exceptions;

    public class NotUpdatedException : Exception
    {
        public NotUpdatedException() : base() { }

        public NotUpdatedException(string message) : base(message) { }

        public NotUpdatedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
;
