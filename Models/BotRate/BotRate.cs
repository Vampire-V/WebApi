using Newtonsoft.Json;

namespace WebApi.Models.BotRate
{
    public class BotRate
    {
        [JsonProperty(PropertyName = "period")]
        public DateTime Period { get; set; }
        [JsonProperty(PropertyName = "currency_id")]
        public string? CurrencyId { get; set; }
        [JsonProperty(PropertyName = "currency_name_th")]
        public string? CurrencyNameTh { get; set; }
        [JsonProperty(PropertyName = "currency_name_eng")]
        public string? CurrencyNameEng { get; set; }
        [JsonProperty(PropertyName = "buying_sight")]
        public Decimal? BuyingSight { get; set; }
        [JsonProperty(PropertyName = "buying_transfer")]
        public Decimal? BuyingTransfer { get; set; }
        [JsonProperty(PropertyName = "selling")]
        public Decimal? Selling { get; set; }
        [JsonProperty(PropertyName = "mid_rate")]
        public Decimal? MidRate { get; set; }
    }
}