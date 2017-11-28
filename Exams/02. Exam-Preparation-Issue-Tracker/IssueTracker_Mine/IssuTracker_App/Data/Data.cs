using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuTracker_App.Data
{
    public class Data
    {
        private static IssueTrackerContext context;

        public static IssueTrackerContext Context
            => context ?? (context = new IssueTrackerContext());
    }
}
