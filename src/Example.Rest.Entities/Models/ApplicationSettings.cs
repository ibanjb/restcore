using Example.Rest.Entities.Interfaces;

namespace Example.Rest.Entities.Models
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string ApplicationInsightsApiKey { get; set; }
    }
}
