using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyWebApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly MyApplicationContext _context;

        public List<User> Users { get; set; }

        public IndexModel(ILogger<IndexModel> logger, MyApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users.ToList();
        }
    }
}
