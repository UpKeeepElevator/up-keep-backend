namespace UpKeep.Data.Configuration;

public class IdentityData
{
    public const string AdminUserPolicyName = "Administrator";
    public const string AdminClaimName = "admin";
    public const string AdminClaimValue = "True";

    public const string SuperAdminPolicyName = "SuperadminPolicy";
    public const string SuperAdminClaimName = "Superadmin";
    public const string SuperAdminClaimValue = "True";

    public const string LaboratorioCodClaimName = "lab";

    public const string NameIdentifierClaimName =
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";


    public static List<string> GetAllClaims() => new List<string>
    {
        AdminClaimName,
        SuperAdminClaimName,
        LaboratorioCodClaimName,
        NameIdentifierClaimName
    };
}