using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.Common.Responses;

namespace IMAOCMS.API;
public sealed class WindowsBackgroundService : BackgroundService
{
    private readonly IRelayCardService _relayCardService;
    private readonly ILogger<WindowsBackgroundService> _logger;
    private bool _isFirst = true;
    private bool _requiredReboot = true;
    private bool _requiredReboot4changed = true;
    private static bool _isNetworkOnline;
    private static bool _oldNetworkValue = true;
    public WindowsBackgroundService(
     IRelayCardService relayCardService,
     ILogger<WindowsBackgroundService> logger) =>
     (_relayCardService, _logger) = (relayCardService, logger);

    //public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger, Microsoft.Extensions.Configuration.IConfiguration config)
    //{
    //    _logger = logger;
    //}
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000).ConfigureAwait(false);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // not bu iki işlemi yapacak reel işlemler apiden alınıp servis hazırlanmalıdır!!!!
                if (_isFirst)
                {
                    //DirectoryInfo file = new("c:\\service");
                    //if (!file.Exists)
                    //{
                    //    file.Create();
                    //}
                    //if (_requiredReboot)
                    //{
                    //    #region Open Browser
                    //    //DoOpenBrowser();
                    //    _logger.LogWarning("Opened browser for first run: {time}", DateTimeOffset.Now);
                    //    #endregion
                    //}
                    _requiredReboot = false;
                    #region Canlı izleme
                    await StartAllDeviceMonitoring();
                    //File.AppendAllText("c:\\service\\data.txt", $"\nstart-monitor-first" + DateTimeOffset.Now.ToString());
                    #endregion
                    _isFirst = false;
                }
                else
                if (_isNetworkOnline && !_oldNetworkValue)
                {
                    _logger.LogWarning("Network changed replay realtime monitoring: {time}", DateTimeOffset.Now);
                    if (_requiredReboot4changed)
                    {
                        #region Open Browser
                        //DoOpenBrowser();
                        #endregion
                    }
                    _requiredReboot4changed = false;
                    #region Canlı izleme
                    await StartAllDeviceMonitoring();
                    //File.AppendAllText("c:\\service\\data.txt", $"\nstart-monitor-reboot" + DateTimeOffset.Now.ToString());
                    #endregion
                    _oldNetworkValue = _isNetworkOnline;
                }


            }
            catch (Exception ex) when (stoppingToken.IsCancellationRequested)
            {

                _logger.LogError($"(Canlı izleme)Sunucu -CancellationRequested:{ex.Message}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"(Canlı izleme)Sunucu:{ex.Message}");
                continue;
            }

            await Task.Delay(10000, stoppingToken);
        }
    }

    private async Task StartAllDeviceMonitoring()
    {
        File.AppendAllText("c:\\service\\data.txt", $"\nstart-monitor" + DateTimeOffset.Now.ToString());
        using HttpClient httpClient = new();
        //string baseurl = "https://localhost:7000/api/";
        string baseurl = "https://localhost:7000/api/StartRelayCard";
            //ConfigurationHelper.Configuration["Kestrel:BaseUrl"];
        //var appconfig=_config.GetSection()
       // HttpResponseMessage data1 = await httpClient.GetAsync(baseurl + "Devices/StopAllRealTimeMonitoring");
        //string str1 = await data1.Content.ReadAsStringAsync();

        //var response = await httpClient.GetFromJsonAsync<ApiDicListResponse>(baseurl + "StartRelayCard");
        var response = await httpClient.GetFromJsonAsync<ApiDicListResponse>(baseurl);

        if (response != null)
        {
            if (response.Status)
            {
                _logger.LogWarning("(Canlı izleme)Tüm cihazlar yeniden canlı izlemeye alındı.", DateTimeOffset.Now);
            }
            else
            {
                _logger.LogError($"(Canlı izleme)Tüm cihazlar yeniden canlı izlemeye alınırken bir hata oluştu: {response.Message}", DateTimeOffset.Now);
            }
        }
        else
        {
            _logger.LogError($"(Canlı izleme)Sunucuya ulaşılamadı", DateTimeOffset.Now);

        }
    }
}
