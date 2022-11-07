namespace PagOnlineAPI.DTO
{
    [Serializable]
    public class MailResponse
    {
        public MailResponse()
        {

        }
        public bool? IsSuccessful { get; set; }
        public string? Result { get; set; }
    }
}
