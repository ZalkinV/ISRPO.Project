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


		public Record(bool isTracking)
		{
			DateTime = DateTime.Now;
			IsTracking = isTracking;
		}

		public Record(bool isTracking, ProcessType processType) : this(isTracking)
		{
			Process = processType;
		}

		public Record(DateTime dateTime, bool isTracking) : this(isTracking)
		{
			DateTime = dateTime;
		}

		public Record(DateTime dateTime, bool isTracking, ProcessType processType) : this(dateTime, isTracking)
		{
			Process = processType;
		}

		public override string ToString()
		{
			return string.Join(",", DateTime.ToString(), IsTracking, Process);
		}
	}
}
