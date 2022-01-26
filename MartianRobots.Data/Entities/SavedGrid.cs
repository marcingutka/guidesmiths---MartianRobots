using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MartianRobots.Data.Entities
{
    public class SavedGrid
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RunId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
