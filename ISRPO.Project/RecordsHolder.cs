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
	}
}
