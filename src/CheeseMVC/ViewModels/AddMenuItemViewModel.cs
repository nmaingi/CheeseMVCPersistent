using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int CheeseID { get; set; }
        public int MenuID { get; set; }
        
        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel()
        {

        }
        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Cheeses = new List<SelectListItem>();

            foreach(var item in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.Name
                });
            }

            Menu = menu;
        }
    }  
}
