namespace EPharmacy.Application.Common.Templates;

public static partial class Templates
{
    public static class Emails
    {
        /// <summary>
        /// Welcome Email sent to new Pharmacy users on account create with the redirect link.
        /// </summary>
        public static string Welcome(Uri redirectLink)
        {
            return ReplaceTextInTemplate(nameof(Welcome), redirectLink.ToString(), TemplateType.Email);
        }

        /// <summary>
        /// Forgot password template page
        /// </summary>
        /// <param name="redirectLink">Link to redirect</param>
        /// <returns>Page with the redirect link</returns>
        public static string ForgotPassword(Uri redirectLink)
        {
            return ReplaceTextInTemplate(nameof(ForgotPassword), redirectLink.ToString(), TemplateType.Email);
        }
    }
}

