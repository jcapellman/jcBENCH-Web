using jcBENCH.MVC.DAL;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jcBENCH.MVC.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController(MainDbContext dbContext) : Controller
    {
        [Route("downloads")]
        public async Task<ActionResult> DownloadsAsync()
        {
            var releases = await dbContext.Releases
                .AsNoTracking()
                .Include(a => a.ReleaseArtifacts)
                .OrderByDescending(a => a.ReleaseDate)
                .ToListAsync();

            return View("Downloads", releases);
        }

        [Route("about")]
        public ActionResult About() => View();

        [Route("")]
        public async Task<ActionResult> IndexAsync() => 
            View("Index", await dbContext.BenchmarkResults
                .AsNoTracking()
                .OrderByDescending(a => a.BenchmarkResult)
                .ToListAsync());

        [Route("archives")]
        public async Task<ActionResult> ArchivesAsync() => 
            View("Archives", await dbContext.BenchmarkResults
                .AsNoTracking()
                .OrderBy(a => a.BenchmarkName)
                .ToListAsync());
    }
}