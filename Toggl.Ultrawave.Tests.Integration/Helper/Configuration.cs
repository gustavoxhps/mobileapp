using Toggl.Ultrawave.Network;

namespace Toggl.Ultrawave.Tests.Integration.Helper
{
    public static class Configuration
    {
        public static UserAgent UserAgent { get; }
            = new UserAgent("MobileIntegrationTests", "c22a18cb7bf1cd3ab25005b0e55919b0c594e9cd");
    }
}
