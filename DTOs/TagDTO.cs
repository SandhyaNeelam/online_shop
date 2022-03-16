using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onlineshop.DTOs;

public record TagDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }
}

public record TagCreateDTO
{

    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

    [JsonPropertyName("product_id")]
    [Required]
    public int ProductId { get; set; }

}