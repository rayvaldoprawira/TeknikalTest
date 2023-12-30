namespace TeknikalTest.Contracts
{
    public interface IEmailHandler
    {
        void SendEmail(string toEmail, string subject, string htmlMessage);
    }
}
