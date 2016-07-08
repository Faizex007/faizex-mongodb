using MongoDB.Driver;

namespace Faizex.MongoDb
{
  /// <summary>
  /// The interface contract for <see cref="MongoAccess"/>.
  /// </summary>
  public interface IMongoAccess
  {
    /// <summary>
    /// Gets the <see cref="IMongoClient"/>.
    /// </summary>
    IMongoClient Client { get; }

    /// <summary>
    /// Gets the <see cref="IMongoDatabase"/>.
    /// </summary>
    IMongoDatabase Database { get; }

    /// <summary>
    /// Gets or sets the <see cref="IMongoProvider{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the mongo provider.</typeparam>
    /// <param name="collectionName">The collection name.</param>
    /// <returns>IMongoProvider{T}</returns>
    IMongoProvider<T> Provider<T>(string collectionName) where T : IMongoDocument;
  }
}