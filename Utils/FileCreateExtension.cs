namespace Exam4.Utils
{
    public static class FileCreateExtension
    {
        public static string CreateFile(IFormFile form, string webroot, string folderName)
        {
            string filename = "";
            if (form.FileName.Length > 100)
            {
                filename = Guid.NewGuid() + form.FileName.Substring(filename.Length - 64);
            }
            else
            {
                filename = Guid.NewGuid() + form.FileName;
            }
            var path = Path.Combine(webroot + folderName, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                form.CopyTo(stream);
            }
            return filename;

        }
    }
}
