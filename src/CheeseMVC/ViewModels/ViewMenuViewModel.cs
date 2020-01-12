using CheeseMVC.Models;
using System.Collections.Generic;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }

        public ViewMenuViewModel()
        { 
        }

    }
}
