using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssuTracker_App.Data;

namespace IssuTracker_App.Services
{
    public abstract class Service
    {
        public Service()
        {
            this.Context = Data.Data.Context;
        }

        protected IssueTrackerContext Context { get; }
    }
}
