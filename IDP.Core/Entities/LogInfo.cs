using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Core.Entities
{
    public class LogInfo
    {
        [BsonId]
        public int id { get; set; }

        public string Message { get; set; }

    }
}
