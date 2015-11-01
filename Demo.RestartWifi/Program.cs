using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Principal;
using System.Threading;
using System.IO;

namespace Demo.RestartWifi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Restart Network Adapter - Call Restart-NetAdapter PowerShell command
			RestartNetworkAdapter();

			// Connect WiFi - Use SimpleWifi
			ConnectWiFi();

			Console.Read();
		}

		/// <summary>
		/// http://blogs.msdn.com/b/kebab/archive/2014/04/28/executing-powershell-scripts-from-c.aspx
		/// didn't work so.
		/// http://picuspickings.blogspot.com/2011/03/elevate-runas-using-c.html
		/// </summary>
		private static void RestartNetworkAdapter()
		{
			WindowsIdentity windowsId = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(windowsId);

			if (principal.IsInRole(WindowsBuiltInRole.Administrator))
			{
				Console.WriteLine("No elevation required, already running as an Admin!");
			}

			// Process.GetCurrentProcess().ProcessName
			ProcessStartInfo process = new ProcessStartInfo();

			FileInfo fileInfo = new FileInfo("RestartWifi.ps1");
			process.FileName = "powershell";
			process.Arguments = fileInfo.FullName;
			process.Verb = "runas";

			try
			{
				Process proc = Process.Start(process);
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		/// <summary>
		/// Event handler for when data is added to the output stream.
		/// </summary>
		/// <param name="sender">Contains the complete PSDataCollection of all output items.</param>
		/// <param name="e">Contains the index ID of the added collection item and the ID of the PowerShell instance this event belongs to.</param>
		private static void outputCollection_DataAdded(object sender, DataAddedEventArgs e)
		{
			// do something when an object is written to the output stream
			Console.WriteLine("Object added to output.");
		}

		/// <summary>
		/// Event handler for when Data is added to the Error stream.
		/// </summary>
		/// <param name="sender">Contains the complete PSDataCollection of all error output items.</param>
		/// <param name="e">Contains the index ID of the added collection item and the ID of the PowerShell instance this event belongs to.</param>
		private static void Error_DataAdded(object sender, DataAddedEventArgs e)
		{
			// do something when an error is written to the error stream
			Console.WriteLine("An error was written to the Error stream!");
		}

		private static void ConnectWiFi()
		{
			
		}
	}
}
