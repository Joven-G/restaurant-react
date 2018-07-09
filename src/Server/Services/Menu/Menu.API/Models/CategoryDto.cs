﻿using System;
using Newtonsoft.Json;

namespace Restaurant.Common.DataTransferObjects
{
    public class CategoryDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}