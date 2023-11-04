namespace PM.WebApi.Common.Congifuratuions.Constants;

/// <summary>
/// 
/// </summary>
public static class DomainApiConstants
{
    /// <summary>
    /// 
    /// </summary>
    public static string TitleApi = "Project Manager API";

    /// <summary>
    /// 
    /// </summary>
    public static string Version = "v1";

    /// <summary>
    /// 
    /// </summary>
    public static string AuthScheme = "Bearer";

    /// <summary>
    /// 
    /// </summary>
    public static string AuthNameHeader = "Authorization";

    /// <summary>
    /// 
    /// </summary>
    public static string AuthDescription = "Авторизация JWT с использованием схемы Bearer Token.\r\n" +
                    "Введите «Bearer», а затем через пробел свой токен в текстовом поле ниже.\r\n" +
                    "\"Пример: \"Bearer {Ваш_Токен}\".\r\n" +
                    "Пример ввода токена в HTTP запрос \"Authorization: Bearer {Ваш_Токен}\"\r\n" +
                    "Токен указывается в \"Header\" HTTP запроса.";

    /// <summary>
    /// 
    /// </summary>
    public static string Cors = "CORS";

    /// <summary>
    /// 
    /// </summary>
    public static string ResourcesPath = "Resources";

    /// <summary>
    /// 
    /// </summary>
    public static string CorsPolicyName = "CorsPolicy";
}
