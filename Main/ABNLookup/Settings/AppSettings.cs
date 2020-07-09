using System.ComponentModel.DataAnnotations;

namespace ABNLookup.Settings
{
    public class AppSettings
    {
        [Required]
        public string AbnJsonFilePath { get; set; }
        [Required]
        public string ConnectionString { get; set; }
        [Required]
        public string ProcessMessagesJsonFilePath { get; set; }
        [Required]
        public string SqLiteDbFilePath { get; set; }
        [Required]
        public bool DisplayExceptionDetails { get; set; }       
        [Required]
        public ApiSettings API { get; set; }
        [Required]
        public SwaggerConfig Swagger { get; set; }
    }

    public class ApiSettings
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public ApiContact Contact { get; set; }

        public string TermsOfServiceUrl { get; set; }

        public ApiLicense License { get; set; }

        public string SmtpServer { get; set; }
        public string FromEmailAddress { get; set; }
        public ApiStatus  Status { get; set; }
        public ApiRetirement RetirementDate { get; set; }
    }

    public class ApiContact
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Url { get; set; }
    }

    public class ApiLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class SwaggerConfig
    {
        [Required]
        public bool Enabled { get; set; }
    }

    public class ApiStatus
    {
        public string  V1 { get; set; }
        public string V2 { get; set; }
    }

    public class ApiRetirement
    {
        public string V1 { get; set; }
        public string V2 { get; set; }
    }
}