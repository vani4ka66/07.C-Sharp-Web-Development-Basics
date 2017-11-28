using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_App.ViewModels
{
    public class AllViewModel
    {
        public IEnumerable<AllGameViewModel> Games { get; set; }

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
