namespace WebDriverProxy.DTO
{
    public class StatusDto
    {
        public string sessionId { get; set; }
        public int status { get; set; }
        public BuildDTO build { get; set; }
        //public OsDTO os { get; set; }
    }
}
