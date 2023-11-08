namespace PM.WebApi.Common.Constants;

/// <summary>
/// Constants related to the Domain API.
/// </summary>
public static class DomainApiConstants
{
    /// <summary>
    /// Title of the API.
    /// </summary>
    public static string TitleApi = "Project Manager API";

    /// <summary>
    /// Version of the API.
    /// </summary>
    public static string Version = "v1";

    /// <summary>
    /// Authentication scheme used (e.g., Bearer).
    /// </summary>
    public static string AuthScheme = "Bearer";

    /// <summary>
    /// Header name for authentication (e.g., Authorization).
    /// </summary>
    public static string AuthNameHeader = "Authorization";

    /// <summary>
    /// Description of JWT authorization using the Bearer Token scheme.
    /// </summary>
    public static string AuthDescription = "Authorization using JWT with the Bearer Token scheme.\r\n" +
                    "Enter 'Bearer,' followed by your token with a space in the text field below.\r\n" +
                    "Example: 'Bearer {Your_Token}'.\r\n" +
                    "Example of including the token in an HTTP request header: 'Authorization: Bearer {Your_Token}'.\r\n" +
                    "The token is specified in the header of the HTTP request.";

    /// <summary>
    /// Name of the CORS (Cross-Origin Resource Sharing) setting.
    /// </summary>
    public static string Cors = "CORS";

    /// <summary>
    /// Path to the API's resources.
    /// </summary>
    public static string ResourcesPath = "Resources";

    /// <summary>
    /// Name of the CORS policy.
    /// </summary>
    public static string CorsPolicyName = "CorsPolicy";
}

