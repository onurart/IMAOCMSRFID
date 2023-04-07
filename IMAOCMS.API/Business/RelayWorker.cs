using IMAOCMS.API.Business.Interfaces;
namespace IMAOCMS.API.Business;
public class RelayWorker : BackgroundService
{
    private readonly ILogger<RFIDWorker> _logger;
    IRelayCardService _relayCardService;
    bool _disposed;
    public RelayWorker(IRelayCardService relayCardService, ILogger<RFIDWorker> logger)
    {
        _relayCardService = relayCardService;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (_disposed)
            {
                var sss = await _relayCardService.ReadStatusWorkAsync();
            }
        }
        catch (TaskCanceledException e)
        {
            _logger.LogError(e, "TaskCanceledException");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception");
        }
    }
    public override Task StartAsync(CancellationToken cancellationToken)
    {

        _logger.LogWarning("Consume Scoped Service Hosted Service is starting.");
        _disposed = true;
        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogWarning("Consume Scoped Service Hosted Service is stopping.");
        _disposed = false;
        //var result = await _relayCardService.StopReadAsync();
        await base.StopAsync(stoppingToken);
    }
}
