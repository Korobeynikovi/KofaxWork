using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SignalRChat.Model;
using System.Collections.Generic;
using System.Diagnostics;

namespace SignalRChat.Controllers
{
	public class TaskController : Controller
	{
		
		[HttpGet]
		[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
		public List<ProcessModel> GetTasks()
		{
			List<ProcessModel> processModels = new List<ProcessModel>();

			
			foreach (var process in Process.GetProcesses())
			{
				try
				{
					processModels.Add(new ProcessModel(
					process.ProcessName,
					process.Id,
					process.PrivateMemorySize64,
					process.StartTime
					));
				}
				catch { }
			}

			return processModels;
		}
	}
}
