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
        public static void OnClick(object sender, EventArgs e)
        {
            var command = sender as OleMenuCommand;
            command.Text = "Clicked!";
        }
    }
}
