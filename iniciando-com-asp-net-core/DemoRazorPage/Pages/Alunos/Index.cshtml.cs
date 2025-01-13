using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoRazorPage.Data;
using DemoRazorPage.Model;

namespace DemoRazorPage.Pages.Alunos
{
    public class IndexModel : PageModel
    {
        private readonly DemoRazorPage.Data.ApplicationDbContext _context;

        public IndexModel(DemoRazorPage.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Aluno> Aluno { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Aluno = await _context.Aluno.ToListAsync();
        }
    }
}
