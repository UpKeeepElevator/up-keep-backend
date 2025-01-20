namespace UpKeep.Data.Configuration;

public class BucketConfig
{
    public string S3Bucket { get; set; }
    public string VultrUrl { get; set; }
    public string AWSAccessKey { get; set; }
    public string AWSSecretKey { get; set; }


    public static string UsersFolder() => $"/Usuarios";
    public static string AveriaFolder() => $"/Averias";
    public static string MantenimientoFolder() => $"/Mantenimiento";
}