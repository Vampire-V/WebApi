namespace WebApi.Models.FaceRecognition
{
    public class FaceRecognitionUpload
    {
        public string EmployeeNo { get; set; } = null!;
        public List<IFormFile> Files { get; set; } = null!;
        public List<string> DetectFace { get; set; } = null!;
    }
}