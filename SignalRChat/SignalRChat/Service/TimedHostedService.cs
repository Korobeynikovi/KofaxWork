using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using SignalRChat.Model;
using System.Diagnostics;

namespace SignalRChat.Service
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private int executionCount = 0;
		private readonly ILogger<TimedHostedService> _logger;
		private Timer? _timer = null;
		private readonly IHubContext<ChatHub> hubContext;
		private List<ProcessModel> processModels = new List<ProcessModel>();
		public List<ProcessModel> ProcessModelProperty { get; private set; }
		public TimedHostedService(ILogger<TimedHostedService> logger, IHubContext<ChatHub> hubContext)
		{
			_logger = logger;
			this.hubContext = hubContext;
		}

		public Task StartAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Timed Hosted Service running.");

			_timer = new Timer(DoWork, null, TimeSpan.Zero,
				TimeSpan.FromSeconds(5));

			return Task.CompletedTask;
		}

		private void DoWork(object? state)
		{
			var count = Interlocked.Increment(ref executionCount);

			PerformanceCounter pc = new ("Процессор", "% загруженности процессора", "_Total");

			Console.WriteLine(pc.NextValue());

			if (pc.NextValue() > 80)
				hubContext.Clients.All.SendAsync("ReceiveMessage", "Внимание ВЫСОКАЯ ЗАГРУЗКА ЦП");

			_logger.LogInformation(
				"Timed Hosted Service is working. Count: {Count}", count);
		}

		public Task StopAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Timed Hosted Service is stopping.");

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
