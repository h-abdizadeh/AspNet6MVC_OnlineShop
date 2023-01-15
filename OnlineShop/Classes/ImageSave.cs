namespace OnlineShop.Classes;

public class ImageSave
{
    public async Task<string> AddImage(IFormFile img, string folder)
    {
        try
        {
            Random r = new Random();
            int prefix = r.Next(1000, 10000);
            string imgName = prefix + Path.GetExtension(img.FileName);

            string imgPath = Path.Combine("wwwroot/image/", folder);
            if (!Directory.Exists(imgPath))
                Directory.CreateDirectory(imgPath);

            string savePath = Path.Combine(imgPath, imgName);
            using (Stream imgStream = new FileStream(savePath, FileMode.Create))
            {
                await img.CopyToAsync(imgStream);
            }

            return await Task.FromResult(imgName);
        }
        catch (Exception e)
        {
            Console.Write("ImageSave AddImage --> {0}", e.Message);
            return await Task.FromResult("");
        }
    }

}
