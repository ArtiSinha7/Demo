using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class SchoolModel
    {
        public class ApiResponse
        {
            [JsonProperty("isSuccess")]
            public bool IsSuccess { get; set; }

            [JsonProperty("returnMessage")]
            public string ReturnMessage { get; set; }

            [JsonProperty("returnCode")]
            public string ReturnCode { get; set; }

            [JsonProperty("data")]
            public List<ProjectData> Data { get; set; }

            [JsonProperty("extraData")]
            public object ExtraData { get; set; }
        }

        public class ProjectData
        {
            public int Id { get; set; }
            public int PrJ_ID { get; set; }

            [JsonProperty("imageURL")]
            public string ImageURL { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }

            [JsonProperty("imageurls")]
            public string imageurls { get; set; }

            public string ImagePath => !string.IsNullOrEmpty(ImageURL) ? ImageURL : image;

            [JsonProperty("textField1")]
            public string textField1 { get; set; }

            [JsonProperty("textField2")]
            public string textField2 { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("productUrl")]
            public string productUrl { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }
        }
    }
}