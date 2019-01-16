using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ISRPO.Project
{
	/// <summary>
	/// Command handler
	/// </summary>
	internal sealed class TimeTracker
	{
		public static RecordsHolder Records;

		/// <summary>
		/// Command ID.
		/// </summary>
		public const int CommandTrackingId = 0x0100;
		public const int CommandShowChartId = 0x0101;

		/// <summary>
		/// Command menu group (command set GUID).
		/// </summary>
		public static readonly Guid CommandSet = new Guid("6e2485ba-73f3-43db-aa3e-b857dad78672");

		/// <summary>
		/// VS Package that provides this command, not null.
		/// </summary>
		private readonly AsyncPackage package;

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeTracker"/> class.
		/// Adds our command handlers for menu (commands must exist in the command table file)
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		/// <param name="commandService">Command service to add command to, not null.</param>
		private TimeTracker(AsyncPackage package, OleMenuCommandService commandService)
		{
			this.package = package ?? throw new ArgumentNullException(nameof(package));
			commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

			Records = new RecordsHolder();

			var menuCommandTrackingId = new CommandID(CommandSet, CommandTrackingId);
            var menuCommandTracking = new OleMenuCommand(TimeTracking.OnClick, menuCommandTrackingId);

			var menuCommandShowGraphId = new CommandID(CommandSet, CommandShowChartId);
			var menuCommandShowGraph = new OleMenuCommand(ChartShowing.OnClick, menuCommandShowGraphId);

            commandService.AddCommand(menuCommandTracking);
			commandService.AddCommand(menuCommandShowGraph);
		}

		/// <summary>
		/// Gets the instance of the command.
		/// </summary>
		public static TimeTracker Instance
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the service provider from the owner package.
		/// </summary>
		private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
		{
			get
			{
				return this.package;
			}
		}

		/// <summary>
		/// Initializes the singleton instance of the command.
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		public static async Task InitializeAsync(AsyncPackage package)
		{
			// Switch to the main thread - the call to AddCommand in TimeTracker's constructor requires
			// the UI thread.
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

			OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
			Instance = new TimeTracker(package, commandService);
		}
	}
}
