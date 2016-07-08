using MongoDB.Driver;

namespace Faizex.MongoDb
{
  /// <summary>
  /// The initial Mongo database access class.
  /// </summary>
  public class MongoAccess : IMongoAccess
  {
    /// <summary>
    /// Gets the <see cref="IMongoClient"/>.
    /// </summary>
    public IMongoClient Client { get; }

    /// <summary>
    /// Gets the <see cref="IMongoDatabase"/>.
    /// </summary>
    public IMongoDatabase Database { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="MongoAccess"/>.
    /// </summary>
    /// <param name="connectionString">The mongo connection string.</param>
    /// <param name="databaseName">The database to access.</param>
    public MongoAccess(string connectionString, string databaseName)
    {
      Client = new MongoClient(connectionString);
      Database = Client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Gets or sets the <see cref="IMongoProvider{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the mongo provider.</typeparam>
    /// <param name="collectionName">The collection name.</param>
    /// <returns>IMongoProvider{T}</returns>
    public IMongoProvider<T> Provider<T>(string collectionName) where T : IMongoDocument
    {
      return new MongoProvider<T>(Database.GetCollection<T>(collectionName));
    }
  }
}