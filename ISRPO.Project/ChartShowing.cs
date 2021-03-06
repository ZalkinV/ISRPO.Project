﻿using System;
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
				Height = 400,
				Width = 400,
			};

			Chart chart = new Chart
			{
				Parent = chartForm,
				Dock = DockStyle.Fill,
			};

			ChartArea chartArea = new ChartArea("AreaWorkingHours");
			chartArea.AxisX.Title = "Dates";
			chartArea.AxisY.Title = "Hours";

			chart.Titles.Add("Working Hours");
			chart.ChartAreas.Add(chartArea);

			TimeTracker.Records.ReadRecords();
			Dictionary<string, double> hoursInDay = GetDatesSeries(TimeTracker.Records);

			Series series = new Series("SeriesWorkingHours");
			series.ChartArea = "AreaWorkingHours";
			series.Points.DataBindXY(hoursInDay.Keys, hoursInDay.Values);
			series.IsValueShownAsLabel = true;
			series["Points Width"] = "1";
			chart.Series.Add(series);

			chartForm.Show();
		}

		private static Dictionary<string, double> GetDatesSeries(RecordsHolder records)
		{
			var result = new Dictionary<string, double>();

			Record prevRecord = records.Records.Values.ElementAt(0);
			string newKey = prevRecord.DateTime.ToString("d");
			double newValue = 0;

			int trackingBorder = 0;

			foreach (var curRecord in records.Records.Values)
			{
				if (curRecord.IsTracking) { trackingBorder++; }
				else { trackingBorder--; }

				if (prevRecord.DateTime.Date != curRecord.DateTime.Date)
				{
					newKey = curRecord.DateTime.ToString("d");
				}

				if (trackingBorder != 1)
				{
					TimeSpan timeSpan = curRecord.DateTime.Subtract(prevRecord.DateTime);
					newValue += timeSpan.TotalHours;
				}

				if (trackingBorder == 0)
				{
					newValue = Math.Round(newValue, 3);
					if (result.ContainsKey(newKey))
					{
						result[newKey] += newValue;
					}
					else
					{
						result.Add(newKey, newValue);
					}
					newValue = 0;
				}

				prevRecord = curRecord;
			}
			return result;
		}
	}
}
