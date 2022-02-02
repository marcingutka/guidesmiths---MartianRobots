using MartianRobots.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MartianRobots.Data.Entities
{
    public class InputData
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RunId { get; set; }
        public Grid Grid { get; set; }
        public List<Robot> Robots { get; set; }
        public List<RobotCommands> Commands { get; set; }
    }
}
