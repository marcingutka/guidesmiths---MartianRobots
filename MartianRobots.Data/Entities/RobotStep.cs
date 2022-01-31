using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.Data.Entities
{
    public class RobotStep
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RunId { get; set; }
        public int RobotId { get; set; }
        public int StepNumber { get; set; }
        public Position Position { get; set; }
        public OrientationState Orientation { get; set; }
        public RectangularMoveCommand? Command { get; set; }
        public bool IsLost { get; set; }
        public bool IsLastStep { get; set; }
    }
}
