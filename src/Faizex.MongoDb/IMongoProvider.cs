using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Faizex.MongoDb
{
  /// <summary>
  /// The interface contract of <see cref="MongoProvider{T}"/>.
  /// </summary>
  /// <typeparam name="T">The type of the collection.</typeparam>
  public interface IMongoProvider<T> where T : IMongoDocument
  {
    /// <summary>
    /// Gets the <see cref="IMongoCollection{TDocument}"/>.
    /// </summary>
    IMongoCollection<T> Collection { get; }

    /// <summary>
    /// Lists many documents.
    /// </summary>
    /// <returns>Task{IEnumerable{T}}</returns>
    Task<IEnumerable<T>> ListAllAsync();

    /// <summary>
    /// Gets a document by Id.
    /// </summary>
    /// <param name="id">The document Id.</param>
    /// <returns>Task{T}</returns>
    Task<T> GetOneByIdAsync(string id);

    /// <summary>
    /// Gets many documents by a list of Ids.
    /// </summary>
    /// <param name="ids">The document Ids.</param>
    /// <returns>Task{IEnumerable{T}}</returns>
    Task<IEnumerable<T>> GetManyByIdAsync(IEnumerable<string> ids);

    /// <summary>
    /// Saves a document.
    /// </summary>
    /// <param name="document">The document to save.</param>
    /// <returns>Task{T}</returns>
    Task<T> SaveOneAsync(T document);

    /// <summary>
    /// Saves many documents.
    /// </summary>
    /// <param name="documents">The documents to save.</param>
    /// <returns>Task{IEnumerable{T}}</returns>
    Task<IEnumerable<T>> SaveManyAsync(IEnumerable<T> documents);

    /// <summary>
    /// Updates a document.
    /// </summary>
    /// <param name="document">The document to update.</param>
    /// <returns>Task{T}</returns>
    Task<T> UpdateOneAsync(T document);

    /// <summary>
    /// Updates many documents.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <returns>Task{IEnumerable{T}}</returns>
    Task<IEnumerable<T>> UpdateManyAsync(IEnumerable<T> document);

    /// <summary>
    /// Deletes a document by Id.
    /// </summary>
    /// <param name="id">The document Id.</param>
    /// <returns>Task{bool}</returns>
    Task<bool> DeleteOneByIdAsync(string id);

    /// <summary>
    /// Deletes many documents by a list of Ids.
    /// </summary>
    /// <param name="ids">The document Ids.</param>
    /// <returns>Task{bool}</returns>
    Task<bool> DeleteManyByIdAsync(IEnumerable<string> ids);
  }
}