using System.Collections.Generic;
using System.Threading.Tasks;
using BioEngine.Core.Entities;
using BioEngine.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly MenuRepository _menuRepository;

        public MenuViewComponent(MenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string key)
        {
            var menu = await _menuRepository.GetByIdAsync(1);

            return View(menu);
        }
    }

    public struct MenuLevel
    {
        public int Level;
        public List<MenuItem> Items;
    }
}