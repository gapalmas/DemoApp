using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DemoApp.Web.DTOs;
using App.Entities;
using App.Infrastructure.Data;
using App.Core.Interfaces;

namespace DemoApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Product> OperationsPro;

        public ProductsController(IMapper Mapper, IOperations<Product> OperationsPro)
        {
            this.Mapper = Mapper;
            this.OperationsPro = OperationsPro;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //var products = await _context.Products.ToListAsync();
            //var products = await Task.Run(() => productRepository.Entities.Where(p => p.Status == true));
            //CREATE
            var products = await OperationsPro.FindAllAsync(p=> p.Status == true);
            //READ
            //UPDATE
            //DELETE
            //Reject Changes
            var model = Mapper.Map<IEnumerable<ProductDTO>>(products);
            return View(model);

            //return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await OperationsPro.FindAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,Status,Stock,StockMin,StockMax")] Product product)
        {
            if (ModelState.IsValid)
            {
                Product _productNew = new Product() { Name = "Producto nuevo", Status = true, Price = 50, Stock = 1, StockMax = 3, StockMin = 2 };
                var products = await OperationsPro.CreateAsync(_productNew);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await OperationsPro.FindAsync(p=>p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,Status,Stock,StockMin,StockMax")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await OperationsPro.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await OperationsPro.FindAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await OperationsPro.FindAsync(m => m.Id == id);
           OperationsPro.Delete(product);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return OperationsPro.Exists(p => p.Id == id);
        }
    }
}
