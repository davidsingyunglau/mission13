using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySQLFun.Models;

namespace MySQLFun.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext _context { get; set; }

        public HomeController(BowlingDbContext temp)
        {
            _context = temp;
        }

        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<Teams> Teams { get; set; }



        public IActionResult Index()
        {
            var blah = _context.Bowlers.ToList();

            ViewBag.Teams = _context.Teams.ToList();

            return View(blah);
        }

        [HttpGet]
        public IActionResult Edit(int BowlerId)
        {
            var bowler = _context.Bowlers.Single(x => x.BowlerID == BowlerId);

            return View("Bowler", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler editInfo)
        {
            _context.Update(editInfo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int BowlerId)
        {
            var bowler = _context.Bowlers.Single(x => x.BowlerID == BowlerId);
            _context.Remove(bowler);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(Bowler bow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bow);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View("Bowler", bow);
            }
        }

        public IActionResult Team(int TeamId)
        {
            var blah = _context.Bowlers
                .Where(x => x.TeamID ==TeamId)
                .ToList();

            ViewBag.Teams = _context.Teams.ToList();
            ViewBag.Team = _context.Teams.Single(x => x.TeamID == TeamId);

            return View(blah);
        }
    }
}
