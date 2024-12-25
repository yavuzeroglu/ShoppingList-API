using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShoppingList.Application.Common.Abstractions.Storage;
using ShoppingList.Infrastructure.Helpers;

namespace ShoppingList.Infrastructure.Services.Storages;

public class AzureStorage : IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient _blobContainerClient;
    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Storage:Azure:AccessUrl"]);
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }

    public async Task<(string pathOrContainer, string fileName)> UploadAsync(string containerName, IFormFile imageFile)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
        
        string fileExtension = Path.GetExtension(imageFile.FileName);
        string fileNewName = $"{NameOperation.ReplaceInvalidChars(imageFile.FileName)}_{DateTime.Now:dd_MM_yyyy}_{DateTime.Now.Millisecond}{fileExtension}";

        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
        await blobClient.UploadAsync(imageFile.OpenReadStream());
        return (containerName, fileNewName);
    }
}