namespace TestAPI.Handlers
{
    public class SecureException : Exception
    {
        public Guid EventId { get; }

        public SecureException(string message) : base(message)
        {
            EventId = Guid.NewGuid();
        }

        public SecureException(string message, Exception innerException) : base(message, innerException)
        {
            EventId = Guid.NewGuid();
        }
    }
}