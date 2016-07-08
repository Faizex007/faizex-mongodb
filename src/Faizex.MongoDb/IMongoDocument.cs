using MongoDB.Bson;

namespace Faizex.MongoDb
{
  /// <summary>
  /// The base Mongo document class.
  /// </summary>
  public interface IMongoDocument
  {
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    ObjectId Id { get; set; }
  }
}