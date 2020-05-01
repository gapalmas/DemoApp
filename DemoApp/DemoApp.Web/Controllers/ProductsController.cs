using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoApp.Web.Data;
using DemoApp.Web.Models.Entities;
using AutoMapper;
using DemoApp.Web.DTOs;
using DemoApp.Web.Data.DAL;

namespace DemoApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper Mapper;
        //private readonly IRepository<Product> productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductsController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork /*IRepository<Product> productRepository*/)
        {
            _context = context;
            Mapper = mapper;
            this.unitOfWork = unitOfWork;
            //this.productRepository = productRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //var products = await _context.Products.ToListAsync();
            //var products = await Task.Run(() => productRepository.Entities.Where(p => p.Status == true));
            //CREATE
            Product _productNew = new Product() { Name = "Producto nuevo", Status = true, Price = 50, Stock = 1, StockMax = 3, StockMin = 2 };
            unitOfWork.ProductRepository.Add(_productNew);
            unitOfWork.Commit();
            //READ
            var products = await Task.Run(() => unitOfWork.ProductRepository.Entities.Where(p => p.Status == true));
            //UPDATE
            Product _product = products.First(p => p.Id == 1);
            _product.Name = "Nombre Modificado";
            unitOfWork.Commit();
            //DELETE
            unitOfWork.ProductRepository.Remove(_product);
            //Reject Changes
            unitOfWork.RejectChanges();
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(product);
                await _context.SaveChangesAsync();
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

            var product = await _context.Products.FindAsync(id);
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
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
