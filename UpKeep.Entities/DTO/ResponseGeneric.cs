namespace UpKeep.Data.DTO;

public class ResponseGeneric
{
    public string Message { get; set; }
    public int StatusCode { get; set; } = 200;
}