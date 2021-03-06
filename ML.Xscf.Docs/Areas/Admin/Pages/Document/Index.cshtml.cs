using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ML.Xscf.Docs.Services;
using Senparc.Scf.Core.Models;

namespace ML.Xscf.Docs.Areas.Admin.Pages.Document
{
    public class IndexModel : BaseAdminPageModel
    {
        private readonly CatalogService _catalogService;

        public IndexModel(CatalogService _catalogService)
        {
            CurrentMenu = "Catalog";
            this._catalogService = _catalogService;
        }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public PagedList<Catalog> Catalogs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            Catalogs = await _catalogService.GetObjectListAsync(PageIndex, 10, _ => true, _ => _.AddTime, Senparc.Scf.Core.Enums.OrderingType.Descending);
        }

        public IActionResult OnPostDelete(string[] ids)
        {
            foreach (var id in ids)
            {
                _catalogService.DeleteObject(_ => _.Id == Convert.ToInt32(id));
            }

            return RedirectToPage("./Index");
        }
    }
}
