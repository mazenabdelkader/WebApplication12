namespace WebApplication12.Data
{
    public class ImageService
    {
       
            public string SaveImage(IFormFile image)
            {
                var folder = "Uploads";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var path = Path.Combine(folder, image.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                image.CopyTo(stream);

                return path;
            }
        
    }
}
