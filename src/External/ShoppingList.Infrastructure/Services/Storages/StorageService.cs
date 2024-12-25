using Microsoft.AspNetCore.Http;
using ShoppingList.Application.Common.Abstractions.Storage;

namespace ShoppingList.Infrastructure.Services.Storages;

public class StorageService : IStorageService
{
   private readonly IStorage _storage;

   public StorageService(IStorage storage)
   {
      _storage = storage;
   }

   public string StorageName { get => _storage.GetType().Name; }

   public Task DeleteAsync(string path, string fileName)
     => _storage.DeleteAsync(path, fileName);

   public async Task<(string path, string fileName)> UploadAsync(IFormFile imageFile, string path)
     => await _storage.UploadAsync(path, imageFile);
   public List<string> GetFiles(string path)
      => _storage.GetFiles(path);

   public bool HasFile(string path, string fileName)
      => _storage.HasFile(path, fileName);

   public Task<(string pathOrContainer, string fileName)> UploadAsync(string pathOrContainer, IFormFile imageFile)
      => _storage.UploadAsync(pathOrContainer, imageFile);

}