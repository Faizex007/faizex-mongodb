using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Faizex.MongoDb
{
  /// <summary>
  /// The initial business logic implementation provider.
  /// </summary>
  /// <typeparam name="T">The type of the collection.</typeparam>
  public class MongoProvider<T> : IMongoProvider<T> where T : IMongoDocument
  {
    /// <summary>
    /// Gets the <see cref="IMongoCollection{TDocument}"/>.
    /// </summary>
    public IMongoCollection<T> Collection { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="MongoProvider{T}"/>.
    /// </summary>
    /// <param name="collection">The mongo collection.</param>
    public MongoProvider(IMongoCollection<T> collection)
    {
      Collection = collection;
    }

    /// <summary>
    /// Lists many documents.
    /// </summary>
    /// <returns>Task{IEnumerable{T}}</returns>
    public async Task<IEnumerable<T>> ListAllAsync()
    {
      return await Collection.Find(f => true).ToListAsync();
    }

    /// <summary>
    /// Gets a document by Id.
    /// </summary>
    /// <param name="id">The document Id.</param>
    /// <returns>Task{T}</returns>
    public async Task<T> GetOneByIdAsync(string id)
    {
      return await Collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Gets many documents by a list of Ids.
    /// </summary>
    /// <param name="ids">The document Ids.</param>
    /// <returns>Task{IEnumerable{T}}</returns>
    public async Task<IEnumerable<T>> GetManyByIdAsync(IEnumerable<string> ids)
    {
      var tasks = ids.Select(GetOneByIdAsync);
      return await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Saves a document.
    /// </summary>
    /// <param name="document">The document to save.</param>
    /// <returns>Task{T}</returns>
    public async Task<T> SaveOneAsync(T document)
    {
      await Collection.InsertOneAsync(document);
      return document;
    }

    /// <summary>
    /// Saves many documents.
    /// </summary>
    /// <param name="documents">The documents to save.</param>
    /// <returns>Task{IEnumerable{T}}</returns>
    public async Task<IEnumerable<T>> SaveManyAsync(IEnumerable<T> documents)
    {
      var tasks = documents.Select(SaveOneAsync);
      return await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <param name="document">The document to update.</param>
    /// <returns>Task{T}</returns>
    public async Task<T> UpdateOneAsync(T document)
    {
      await Collection.ReplaceOneAsync(new BsonDocument("_id", document.Id), document);
      return document;
    }

    /// <summary>
    /// Updates many documents.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <returns>Task{IEnumerable{T}}</returns>
    public async Task<IEnumerable<T>> UpdateManyAsync(IEnumerable<T> document)
    {
      var tasks = document.Select(UpdateOneAsync);
      return await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Deletes a document by Id.
    /// </summary>
    /// <param name="id">The document Id.</param>
    /// <returns>Task{bool}</returns>
    public async Task<bool> DeleteOneByIdAsync(string id)
    {
      var result = await Collection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
      return result.DeletedCount > 0;
    }

    /// <summary>
    /// Deletes many documents by a list of Ids.
    /// </summary>
    /// <param name="ids">The document Ids.</param>
    /// <returns>Task{bool}</returns>
    public async Task<bool> DeleteManyByIdAsync(IEnumerable<string> ids)
    {
      var tasks = ids.Select(DeleteOneByIdAsync);
      var results = await Task.WhenAll(tasks);
      return results.All(f => f);
    }
  }
}