namespace ShoppingList.Application.Common.Abstractions.Storage;

public interface IStorageService : IStorage
{
  public string StorageName { get; }
}