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
		public SortedList<DateTime, Record> Records { get; }
		private int LastRecordInFileIndex = 0;

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
					LastRecordInFileIndex++;
				}
			}
		}

		public void WriteRecords()
		{
			using (StreamWriter sw = new StreamWriter(FilePath, true))
			{
				foreach (var record in Records.Skip(LastRecordInFileIndex))
				{
					sw.WriteLine(record.Value);
					LastRecordInFileIndex++;
				}
			}
			Records.Clear();
		}

		~RecordsHolder()
		{
			if (Records.Last().Value.IsTracking)
			{
				AddRecord(new Record(false));
			}

			WriteRecords();
		}
	}
}
