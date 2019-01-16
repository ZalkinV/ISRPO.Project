using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

		public void ReadRecords()
		{
			using (StreamReader sr = new StreamReader(FilePath))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] fields = line.Split(Record.FieldSeparator.ToCharArray());

					Record newRecord = new Record(
						DateTime.Parse(fields[0]),
						Convert.ToBoolean(fields[1]),
						(ProcessType)Enum.Parse(typeof(ProcessType), fields[2])
					);

					AddRecord(newRecord);
				}
			}
		}

		public void WriteRecords()
		{
			using (StreamWriter sw = new StreamWriter(FilePath, false))
			{
				foreach (var record in Records)
				{
					sw.WriteLine(record.Value);
				}
			}
		}
	}
}
