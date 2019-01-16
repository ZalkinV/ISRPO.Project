using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace ISRPO.Project
{
	class TimeTracking
	{
		private static bool isTracking = false;

		private static RecordsHolder records = new RecordsHolder();

		public static void OnClick(object sender, EventArgs e)
		{
			var command = sender as OleMenuCommand;
			SwitchButton(command);
			records.AddRecord(new Record(isTracking));
		}

		private static void SwitchButton(OleMenuCommand button)
		{
			isTracking = !isTracking;
			if (isTracking)
			{
				button.Text = "Stop tracking";
			}
			else
			{
				button.Text = "Start tracking";
			}
		}
	}
}
