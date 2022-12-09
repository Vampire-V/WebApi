using Newtonsoft.Json;

namespace WebApi.Models.User
{
    public class LineUserProfile
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
        [JsonProperty("pictureUrl")]
        public string? PictureUrl { get; set; }
        [JsonProperty("statusMessage")]
        public string? StatusMessage { get; set; }
    }
}