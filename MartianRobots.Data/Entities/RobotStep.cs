using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MartianRobots.Models;

namespace MartianRobots.Data.Entities
{
    public class RobotStep
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RunId { get; set; }
        public int RobotId { get; set; }

        public GridPosition Position { get; set; }
        public string Command { get; set; }
        public bool IsLost { get; set; }

    }
}
