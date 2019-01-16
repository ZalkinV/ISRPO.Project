using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISRPO.Project
{
	class RecordsHolder
	{
		private string FilePath { get; }
		private SortedList<DateTime, Record> Records { get; }

		public RecordsHolder()
		{
			FilePath = "./TimeTracker.txt";
			Records = new SortedList<DateTime, Record>();
		}

		public RecordsHolder(string filePath) : this()
		{
			FilePath = filePath;
		}

		public void AddRecord(Record record)
		{
			Records.Add(record.DateTime, record);
		}
	}
}
