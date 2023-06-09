using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class ImageData
    {
        [JsonProperty("drivers")]
        public List<ImageDataFields> Data = new List<ImageDataFields>();
    }

    internal class ImageDataFields
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("fullname")]
        public string Name;

        [JsonProperty("encoding")]
        public float[] Vector;

        public ImageDataFields(string id, string name, float[] vector)
        {
            Id = id;
            Name = name;
            Vector = vector;
        }
    }
}
