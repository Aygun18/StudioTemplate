using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioTemplate.DAL;
using StudioTemplate.Models;
using StudioTemplate.Utilities.Extensions;
using StudioTemplate.ViewModels;

namespace StudioTemplate.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AutoValidateAntiforgeryToken]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context,IWebHostEnvironment env)
        {
            _context=context;
            _env=env;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _context.Teams.ToListAsync();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamVM createTeamVM)
        {
           
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createTeamVM.Photo.CheckFileType(createTeamVM.Photo.ContentType)) 
            {
                ModelState.AddModelError("Photo", "Fayl formata uygun deyil");
                return View();
            }
            if (!createTeamVM.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Fayl'in hecmi boyukdur");
                return View();
            }
            Team team = new Team()
            {
                Name = createTeamVM.Name,
                Position = createTeamVM.Position,
                Image=await createTeamVM.Photo.CreateFileAsync(_env.WebRootPath,"assets/img"),
                FacebookLink=createTeamVM.FacebookLink,
                TwitterLink=createTeamVM.TwitterLink,
                LinkedinLink=createTeamVM.LinkedinLink,
            };
            team.Image = await createTeamVM.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id < 1) return BadRequest();
            Team team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) return NotFound();
            UpdateTeamVM updateTeamVM = new UpdateTeamVM()
            {
                Name = team.Name,
                Position = team.Position,
                Image = team.Image,

            };
            return View(updateTeamVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateTeamVM updateTeamVM)
        {
            if (id == null || id < 1) return BadRequest();
            Team team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) return NotFound();
            if (updateTeamVM.Photo != null)
            {
                if (!updateTeamVM.Photo.CheckFileType(updateTeamVM.Photo.ContentType))
                {
                    ModelState.AddModelError("Photo", "Fayl formata uygun deyil");
                    return View();
                }
                if (!updateTeamVM.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Fayl'in hecmi boyukdur");
                    return View();
                }
                team.Image.DeleteFile(_env.WebRootPath, "assets/img");
                team.Image = await updateTeamVM.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            }
            team.Name = updateTeamVM.Name;
            team.Position = updateTeamVM.Position;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if(id==null||id<1) return BadRequest();
            Team team=await _context.Teams.FirstOrDefaultAsync(t => t.Id==id);
            if (team == null) return NotFound();
            team.Image.DeleteFile(_env.WebRootPath, "assets/img");
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
