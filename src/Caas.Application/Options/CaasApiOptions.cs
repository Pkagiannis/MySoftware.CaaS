namespace Caas.Application.Options;

public class CaasApiOptions
{
    public const string SectionName = "CaasApi";

    [Required]
    public required Uri Uri { get; set; }

    [Required]
    public required string ApiKey { get; set; }
}
