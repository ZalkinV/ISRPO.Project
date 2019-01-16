using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISRPO.Project
{
	enum ProcessType { Coding, Debug, Testing}

	class Record
	{
		public DateTime DateTime { get; }
		public bool IsTracking { get; }
		public ProcessType Process { get; }


		public Record() { }

		public Record(DateTime dateTime, bool isTracking)
		{
			DateTime = dateTime;
			IsTracking = isTracking;
		}

		public Record(DateTime dateTime, bool isTracking, ProcessType processType)
		{
			DateTime = dateTime;
			IsTracking = isTracking;
			Process = processType;
		}
	}
}
