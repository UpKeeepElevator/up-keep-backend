namespace UpKeep.Data.DTO.Core.Cliente;

public class EdificioRequest
{
    public string Edificio1 { get; set; } = null!;
    public string? EdificioUbicacion { get; set; }
    public string Geolocalizacion { get; set; } = null!;
    public int ClienteId { get; set; }
}