namespace EPharmacy.Application.Common.Templates;

public static partial class Templates
{
    public static class TextMessages
    {
        /// <summary>
        /// Message sent to pharmacy administrators when there's a new prescription.
        /// </summary>
        public static string NewPrescription(string userName) => ReplaceTextInTemplate(nameof(NewPrescription), userName, TemplateType.TextMessage);

        /// <summary>
        /// Dispatch message sent to patients when their prescription has been dispatched.
        /// </summary>
        public static string Dispatch(string userName) => ReplaceTextInTemplate(nameof(Dispatch), userName, TemplateType.TextMessage);
    }
}

