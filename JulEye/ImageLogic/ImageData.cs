using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.ImageLogic
{
    internal class ImageData
    {
        [JsonProperty("drivers")]
        private List<ImageDataFields> _data;

        public IReadOnlyList<ImageDataFields> Data => _data;
    }

    internal class ImageDataFields 
    {
        [JsonProperty("id")]
        private string _id;

        [JsonProperty("fullname")]
        private string _name;

        [JsonProperty("encoding")]
        private float[] _vector;

        public string Id => _id;
        public string Name => _name;
        public float[] Vector => _vector;

        public ImageDataFields(string id, string name, float[] vector)
        {
            _id = id;
            _name = name;
            _vector = vector;
        }
    }
}
