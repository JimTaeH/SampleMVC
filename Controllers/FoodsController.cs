using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodApp.Data;
using FoodApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodApp.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Foods/CalculateBMR/
        public async Task<IActionResult> BMRForm() 
        {
            return View("BMRForm");
        }
        // GET: Foods/BMRCalculated/
        public async Task<IActionResult> BMRCalculated(string gender, double weight, double height, double age) 
        {
            double bmrRes;

            if (gender == "Male") 
            {
                bmrRes = 66 + (13.7 * weight) + (5 * height) - (6.8 * age);
            }
            else
            {
                bmrRes = 665 + (9.6 * weight) + (1.8 * height) - (4.7 * age);
            }

            ViewBag.Weight = weight;
            ViewBag.Height = height;
            ViewBag.Age = age;
            ViewBag.Gender = gender;
            ViewBag.Result = $"พลังงานที่จำเป็นในแต่ละวันของคุณคือ {bmrRes} kcal";
            //int eatKcal = (int)bmrRes / 3;

            return View("BMRResult", await _context.Food.Where(f => f.FoodCal.Value < ((int)bmrRes / 3)).ToListAsync());
        }


        // GET: Foods/RandomFood/
        public async Task<IActionResult> RandomFood()
        {
            var randFood = new List<string>();
            randFood.Add("ข้าว");
            randFood.Add("ปู");
            randFood.Add("เนื้อ");
            randFood.Add("กะเพรา");
            randFood.Add("ด้ง");

            Random rnd = new Random();
            ViewBag.RandomValue = randFood[rnd.Next(0,randFood.Count)];

            return View("RandomFood");
        }

        // POST: Foods/Result/
        public async Task<IActionResult> RandomResult(string randomPhrase) 
        {
            return View("RandomResult", await _context.Food.Where(f => f.FoodName.Contains(randomPhrase)).ToListAsync());
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {

              return _context.Food != null ? 
                          View(await _context.Food.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Food'  is null.");
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,FoodImg,FoodDesc,FoodCost,FoodCal")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,FoodImg,FoodDesc,FoodCost,FoodCal")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Food == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Food'  is null.");
            }
            var food = await _context.Food.FindAsync(id);
            if (food != null)
            {
                _context.Food.Remove(food);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
          return (_context.Food?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
