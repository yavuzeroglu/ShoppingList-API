using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShoppingList.Application.Common.Abstractions.Storage;
using ShoppingList.Infrastructure.Helpers;

namespace ShoppingList.Infrastructure.Services.Storages;


public class LocalStorage : ILocalStorage
{
   private readonly IWebHostEnvironment _env;
   private const string folderPath = "images";

   public LocalStorage(IWebHostEnvironment env)
   {
      _env = env;
   }

   public async Task DeleteAsync(string pathOrContainer, string fileName)
      => File.Delete($"{pathOrContainer}\\{fileName}");

   public async Task<(string pathOrContainer, string fileName)> UploadAsync(string pathOrContainer, IFormFile imageFile)
   {
      if (!Directory.Exists($"{_env.WebRootPath}/{folderPath}"))
         Directory.CreateDirectory($"{_env.WebRootPath}/{folderPath}");

      var allowedExtension = new[] { ".jpg", ".png", ".jpeg" };
      var fileExtension = Path.GetExtension(imageFile.FileName);
      if (!allowedExtension.Contains(fileExtension))
         throw new ArgumentException("Only '.jpg' - '.jpeg' -'.png' formats are allowed ");

      if (imageFile.Length > 2 * 1024 * 1024)
         throw new InvalidOperationException("Image size cannot be larger than 2MB.");

      string name = NameOperation.ReplaceInvalidChars(imageFile.FileName).ToLower();

      string fileNewName = $"{name}_{DateTime.Now.Millisecond}{Path.GetExtension(imageFile.FileName)}";

      pathOrContainer = Path.Combine($"{_env.WebRootPath}/{folderPath}", fileNewName);

      (string path, string fileName) data = new();
      await using FileStream stream = new(pathOrContainer, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
      await imageFile.CopyToAsync(stream);
      await stream.FlushAsync();

      data.fileName = fileNewName;
      data.path = pathOrContainer;

      return data;
   }

   public List<string> GetFiles(string pathOrContainer)
   {
      DirectoryInfo directory = new(pathOrContainer);
      return directory.GetFiles().Select(f => f.Name).ToList();
   }

   public bool HasFile(string pathOrContainer, string fileName)
   {
      StringBuilder strBuilder = new();
      strBuilder.Append(pathOrContainer);
      strBuilder.Append("\\");
      strBuilder.Append(fileName);
      return File.Exists(strBuilder.ToString());
   }
}
