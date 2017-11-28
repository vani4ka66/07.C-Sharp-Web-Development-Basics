using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_App.Data
{
    public class Data
    {
        private static GameStoreContext context;

        public static GameStoreContext Context
            => context ?? (context = new GameStoreContext());
    }
}
