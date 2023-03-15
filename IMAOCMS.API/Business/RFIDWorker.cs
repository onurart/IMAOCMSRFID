using IMAOCMS.API.Business.Interfaces;

namespace IMAOCMS.API.Business
{
    public class RFIDWorker : BackgroundService
    {
        private readonly ILogger<RFIDWorker> _logger;
        IChafone718Service _chafone718Service;
        bool _disposed;
        public RFIDWorker(IChafone718Service chafone718Service, ILogger<RFIDWorker> logger)
        {
            _logger = logger;
            _chafone718Service = chafone718Service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //while (!stoppingToken.IsCancellationRequested)
                //{
                //    // await Task.Delay(GetDelayTime(), stoppingToken);
                //    _logger.LogInformation("TimerService is working.");
                //    //nextRunTime = GetNextDate();
                //    //_logger.LogWarning("Consume Scoped Service Hosted Service running.");
                //    //await DoWork(stoppingToken);
                //}

                while (_disposed)
                {
                    //await _chafone718Service.StartEpcReader();
                    //Task.Delay(1000).Wait();
                   var sss= await _chafone718Service.StartRead2Async();
                    
                }




            }
            catch (TaskCanceledException e)
            {

                _logger.LogError(e, "Error");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        private async Task DoWork(CancellationToken stoppingToken)
        {
            //using (var scope = Services.CreateScope())
            //{
            //    var scopedProcessingService =
            //    scope.ServiceProvider
            //        .GetRequiredService<IScopedProcessingService>();

            //    await scopedProcessingService.DoWork(stoppingToken);
            //}

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
            var result = await _chafone718Service.StopReadAsync();
            await base.StopAsync(stoppingToken);
        }
    }
}
