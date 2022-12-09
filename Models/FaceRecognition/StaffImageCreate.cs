namespace WebApi.Models.FaceRecognition
{
    public class StaffImageCreate
    {
        public int Id { get; set; }
        public string Employee { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}