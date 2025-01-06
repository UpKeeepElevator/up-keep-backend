namespace UpKeep.Data.Configuration;

public class IdentityData
{
    public const string AdminUserPolicyName = "Administrator";
    public const string AdminRoleClaimName = "admin";
    public const string AdminRoleClaimValue = "True";

    public const string TecnicoRolePolicyName = "TecnicoPolicy";
    public const string TecnicoRoleClaimName = "Tecnico";
    public const string TecnicoRoleClaimValue = "True";

    public const string CLienteRolePolicyName = "ClientePolicy";
    public const string ClienteRoleClaimName = "Cliente";
    public const string ClienteRoleClaimValue = "True";


    public const string NameIdentifierClaimName =
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";


    public static List<string> GetAllClaims() => new List<string>
    {
        AdminRoleClaimName,
        TecnicoRoleClaimName,
        NameIdentifierClaimName,
        ClienteRoleClaimName
    };
}