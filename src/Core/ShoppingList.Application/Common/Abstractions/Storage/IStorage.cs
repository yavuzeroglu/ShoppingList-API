using Microsoft.AspNetCore.Http;

namespace ShoppingList.Application.Common.Abstractions.Storage;

public interface IStorage
{
   Task<(string pathOrContainer, string fileName)> UploadAsync(string pathOrContainer, IFormFile imageFile);
   Task DeleteAsync(string pathOrContainer, string fileName);
   List<string> GetFiles(string pathOrContainer);
   bool HasFile(string pathOrContainer, string fileName);
}