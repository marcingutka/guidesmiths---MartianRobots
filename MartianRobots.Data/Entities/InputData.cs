using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.Data.Entities
{
    public class InputData
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RunId { get; set; }
        public string Name { get; set; }
        public Grid Grid { get; set; }
        public IEnumerable<Robot> Robots { get; set; }
        public IEnumerable<RobotCommands> Commands { get; set; }
    }
}
