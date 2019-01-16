using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ISRPO.Project
{
	class ChartShowing
	{
		public static void OnClick(object sender, EventArgs e)
		{
			Form chartForm = new Form
			{
				Text = "Work Chart",
			};

			Chart chart = new Chart
			{
				Parent = chartForm,
				Dock = DockStyle.Fill,
			};

			chart.Titles.Add("Working Hours");

			chartForm.Show();
		}
	}
}
