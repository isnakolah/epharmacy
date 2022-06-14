namespace EPharmacy.Application.Common.Templates;

public static partial class Templates
{
    private const string REDIRECT_LINK = "#RedirectLink";
    private const string USER_NAME = "#UserName";

    private static string ReplaceTextInTemplate(string fileName, string replaceStr, TemplateType type)
    {
        var createdTemplate = type switch
        {
            TemplateType.Email => File.ReadAllText($"./wwwroot/Templates/Email/{fileName}.html").Replace(REDIRECT_LINK, replaceStr),
            TemplateType.TextMessage => File.ReadAllText($"./wwwroot/Templates/Message/{fileName}.html").Replace(USER_NAME, replaceStr),
            _ => string.Empty,
        };

        return createdTemplate;
    }

    private enum TemplateType
    {
        Email, TextMessage
    }
}
