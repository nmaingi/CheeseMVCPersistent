using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
     
        public IActionResult Index()
        {
            List<Menu> mmenus = context.Menus.ToList();

            return View(mmenus);
        }

        //add to menu
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }


        //handle post request
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }


        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                .CheeseMenus.Include(item => item.Cheese).
                Where(cm => cm.MenuID == id)
                .ToList();

            Menu menu
                 = context.Menus.Single(vm => vm.ID == id);
            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };
            
            return View(viewModel);
        }

       
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(a => a.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();

            return View(new AddMenuItemViewModel (menu, cheeses));
        }
        
        
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuID;

                IList<CheeseMenu> existingItems = context.CheeseMenus
                .Where(cm => cm.CheeseID == cheeseID)
                .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };

                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();
                }

                return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
            }
            return View(addMenuItemViewModel);

        }

    }
}