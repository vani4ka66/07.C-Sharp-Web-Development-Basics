using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_App.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<IndexGameViewModel> Games { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var allGameViewModel in this.Games)
            {
                sb.Append(allGameViewModel);
            }

            return sb.ToString();
        }
    }
}
