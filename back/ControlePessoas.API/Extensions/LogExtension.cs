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

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Elasticsearch([new Uri("https://localhost:9200")], opts =>
                    {
                        opts.DataStream = new DataStreamName("logs", "controlepessoas", "dev");
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
                        transport.Authentication(new BasicAuthentication("elastic", "U24efQYc7904d9zcvh_j"));
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
