using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Serilog;

namespace ControlePessoas.API.Extensions;

public static class LogExtension
{
    public static IHostBuilder AddLogConfiguration(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging((context, logging) =>
        {
            var configuration = context.Configuration;

            var elasticSection = configuration.GetSection("ElasticSearch");
            var uri = elasticSection["Uri"];
            var username = elasticSection["Username"];
            var password = elasticSection["Password"];
            var environment = elasticSection["Environment"] ?? "dev";

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Elasticsearch([new Uri(uri)], opts =>
                    {
                        opts.DataStream = new DataStreamName("logs", "controlepessoas", environment);
                        opts.BootstrapMethod = BootstrapMethod.Failure;
                        opts.ConfigureChannel = channelOpts =>
                        {
                            channelOpts.BufferOptions = new BufferOptions
                            {
                                ExportMaxRetries = 3,
                                InboundBufferMaxSize = 1000
                            };
                        };
                    }, transport =>
                    {
                        transport.Authentication(new BasicAuthentication(username, password));
                        transport.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);
                    })
                    .CreateLogger();

                Log.Information("Serilog inicializado com Elasticsearch.");
            }
            catch (Exception ex)
            {
                Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.File("logs/fallback_log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

                Log.Error(ex, "Falha ao inicializar logs no Elasticsearch. Usando logs locais.");
            }

            logging.AddSerilog();
        });

        return hostBuilder;
    }
}
