namespace WebApi.Extensions
{
    public static class ManageFiles
    {
        public static string Save(string path, IFormFile file)
        {
            string folderName = Path.Combine(@"wwwroot", path);
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            string imagePath = Path.Combine(folderName, file.FileName);
            using (FileStream stream = System.IO.File.Create(imagePath))
            {
                file.CopyTo(stream);
            }
            return imagePath;
        }

        public static bool Remove(string path)
        {
            string folderName = Path.Combine(@"", path);
            var ss = File.Exists(folderName);
            if (!File.Exists(folderName))
            {
                return false;
            }

            File.Delete(folderName);
            // string imagePath = Path.Combine(folderName, file.FileName);
            // using (FileStream stream = System.IO.File.Create(imagePath))
            // {
            //     file.CopyTo(stream);
            // }
            return true;
        }
    }
}