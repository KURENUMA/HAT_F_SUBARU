namespace HAT_F_api.Services
{
    public class HatFApiServiceException : Exception
    {
        public string ForUserMessage { get; set; }

        public HatFApiServiceException() { }
        public HatFApiServiceException(string exceptionMessage) : base(exceptionMessage) { }
        public HatFApiServiceException(string exceptionMessage, Exception exception) : base(exceptionMessage, exception) { }

        public HatFApiServiceException(string exceptionMessage, string forUserMessage) : base(exceptionMessage) { this.ForUserMessage = ForUserMessage; }
        public HatFApiServiceException(string exceptionMessage, string forUserMessage, Exception exception) : base(exceptionMessage, exception) { this.ForUserMessage = ForUserMessage; }

    }
}
