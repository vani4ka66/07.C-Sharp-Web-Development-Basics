using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.Data;

namespace GameStore_App.Services
{
    public abstract class Service
    {
        public Service()
        {
            this.Context = Data.Data.Context;
        }

        protected GameStoreContext Context { get; }
    }
}
