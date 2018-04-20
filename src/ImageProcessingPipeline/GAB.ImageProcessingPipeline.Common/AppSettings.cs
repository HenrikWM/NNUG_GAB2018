using System.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.WebJobs.Host;

namespace GAB.ImageProcessingPipeline.Common
{
    public static class AppSettings
    {
        public static readonly string FunctionsKey = ConfigurationManager.AppSettings["FunctionsKey"];
        public static readonly string IngressFunctionBaseUrl = ConfigurationManager.AppSettings["IngressFunctionBaseUrl"];
        public static readonly string BlobStorageConnectionString = ConfigurationManager.AppSettings[Constants.BlobStorageConnectionName];
        public static readonly string ApplicationInsightsInstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
        public static readonly string CognitiveServicesSubscriptionKey = ConfigurationManager.AppSettings["CognitiveServicesSubscriptionKey"];
        public static readonly string CognitiveServicesApiRoot = ConfigurationManager.AppSettings["CognitiveServicesApiRoot"];

        public static void TelemetryClientTraceAll(TelemetryClient telemetryClient)
        {
            foreach (string appSettingKey in ConfigurationManager.AppSettings.Keys)
            {
                telemetryClient.TrackTrace(
                    $"Loaded appSetting: '{appSettingKey}', " +
                    $"value: '{ConfigurationManager.AppSettings[appSettingKey]}'");
            }
        }

        public static void TrackWriterLogAll(TraceWriter log)
        {
            foreach (string appSettingKey in ConfigurationManager.AppSettings.Keys)
            {
                log.Info(
                    $"Loaded appSetting: '{appSettingKey}', " +
                    $"value: '{ConfigurationManager.AppSettings[appSettingKey]}'");
            }
        }
    }
}
