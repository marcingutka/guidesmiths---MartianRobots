﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MartianRobots.Data.Entities
{
    public class DataName
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid RunId { get; set; }
        public string Name { get; set; }
    }
}
