﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zaliczenia.Data;
using zaliczenia.Models;

namespace zaliczenia.Controllers
{
    public class AutoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Autoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auto.ToListAsync());
        }

        // GET: Autoes/Search
        public async Task<IActionResult> Search()
        {
            return View();
        }

        // POST: Autoes/SearchResults
        public async Task<IActionResult> SearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Auto.Where(a=>a.Nazwa.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Autoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Auto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // GET: Autoes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marka,Nazwa")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auto);
        }

        // GET: Autoes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Auto.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            return View(auto);
        }

        // POST: Autoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marka,Nazwa")] Auto auto)
        {
            if (id != auto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoExists(auto.Id))
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
            return View(auto);
        }

        // GET: Autoes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Auto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // POST: Autoes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auto = await _context.Auto.FindAsync(id);
            if (auto != null)
            {
                _context.Auto.Remove(auto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoExists(int id)
        {
            return _context.Auto.Any(e => e.Id == id);
        }
    }
}
