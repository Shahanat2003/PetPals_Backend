﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.ProductDto;

namespace WebApplication7_petPals.Services.Products
{
    public class ProductService : IProductInterface
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string HostUrl;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(AppDbContext appDbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _context = appDbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];

        }

        public async Task<List<OutPrdctDto>> GetAllProducts()
        {
            try
            {
                var prdct = await _context.products.Include(p => p.Category).ToListAsync();
                if (prdct.Count > 0)
                {
                    var productCategory = prdct.Select(p => new OutPrdctDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        NewPrice = p.NewPrice,
                        OldPrice = p.OldPrice,
                        Type = p.Type,
                        Category = p.Category.Name,
                        Img = HostUrl + p.Img
                    }).ToList();
                    return productCategory;
                }
                return [];

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("exeption in try to fetch all products");

            }

        }
        public async Task<bool> CreateProduct(CreatePrdctDto productDto, IFormFile image)
        {
            try
            {
                string ProductImage = null;
                if (image != null && image.Length > 0)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    ProductImage = "/Images/Products/" + fileName;

                }
                else
                {
                    ProductImage = "/Images/Products/placeholder.jpg";

                }

                var product = _mapper.Map<Product>(productDto);
                product.Img = ProductImage;
                await _context.products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public async Task<string> UpdateProduct(int id, CreatePrdctDto prdctDto, IFormFile image)
        {
            try
            {
                var product = _context.products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.Name = prdctDto.Name;
                    product.Description = prdctDto.Description;
                    product.NewPrice = prdctDto.NewPrice;
                    product.OldPrice = prdctDto.OldPrice;
                    product.CategoryId = prdctDto.CategoryId;
                    prdctDto.Type = prdctDto.Type;
                    if (image != null && image.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        product.Img = "/Images/Products/" + fileName;

                    }
                    else
                    {
                        product.Img = "no imgae found";
                    }
                    await _context.SaveChangesAsync();
                    return "item updated";


                }
                else
                {
                    return "item not found";
                }


            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public async Task<List<OutPrdctDto>> GetProductById(int id)
        {
            try
            {
                var prodct = await _context.products.Where(p => p.Id == id).Select(p => new OutPrdctDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Type = p.Type,
                    NewPrice = p.NewPrice,
                    OldPrice = p.OldPrice,
                    Category = p.Category.Name
                }).ToListAsync();
                return prodct;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }


        }
        public async Task<List<OutPrdctDto>> GetProductByName(string name)
        {
            try
            {
                var prodct = await _context.products
                    .Where(p => p.Category.Name == name)
                    .Select(p => new OutPrdctDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Type = p.Type,
                    NewPrice = p.NewPrice,
                    OldPrice = p.OldPrice,
                    Category = p.Category.Name
                }).ToListAsync();
                return prodct;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }

        }
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var prdct = await _context.products.FirstOrDefaultAsync(p => p.Id == id);
                if (prdct != null)
                {
                    _context.products.Remove(prdct);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}