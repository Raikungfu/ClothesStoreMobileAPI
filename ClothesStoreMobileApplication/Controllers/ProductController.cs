﻿using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace ClothesStoreMobileApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetListCategoryAndProductOptions")]
        public IActionResult GetListCategoryAndProductOptions()
        {
            var productOptionsList = _unitOfWork.ProductOption.GetAll().Select(o => new
            {
                o.ProductOptionsId,
                o.Name,
                o.NameDescription,
            });

            var categoryList = _unitOfWork.Category.GetAll().Select(o => new
            {
                o.CategoryId,
                o.Name,
            });

            return Ok(new
            {
                categoryList,
                productOptionsList
            });
        }
        [HttpGet]
        public IActionResult Get(string orderBy = "Default", int pageNumber = 1, int pageSize = 10, int? categoryId = null, int? sellerId = null, string? name = null, long? priceFrom = null, long? priceTo = null, [FromQuery] List<int> listOptionId = null, [FromQuery] List<int> listCategoryId = null)
        {
            IEnumerable<Product> products;

            if (listOptionId != null && listOptionId.Count() > 0)
            {
                products = _unitOfWork.Product.GetAll(null, null, "Reviews,Options").Where(p => p.Options.Any(o => listOptionId.Contains(o.OptionId)));
            }
            else
            {
                products = _unitOfWork.Product.GetAll(null, null, "Reviews");
            }


            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            if (sellerId.HasValue)
            {
                products = products.Where(p => p.SellerId == sellerId.Value);
            }

            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            if (priceFrom != null)
            {
                products = products.Where(p => p.NewPrice >= priceFrom);
            }

            if (priceTo != null)
            {
                products = products.Where(p => p.NewPrice <= priceTo);
            }

            if (listCategoryId != null && listCategoryId.Count() > 0)
            {
                products = products.Where(p => p.CategoryId.HasValue && listCategoryId.Contains(p.CategoryId.Value));
            }

            switch (orderBy)
            {
                case "SaleOff":
                    products = products.OrderByDescending(p => p.NewPrice < p.OldPrice ? (p.OldPrice - p.NewPrice) : 0);
                    break;
                case "Newest":
                    products = products.OrderByDescending(p => p.ProductId);
                    break;
                case "BestSeller":
                    products = products.OrderByDescending(p => p.QuantitySold);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductId);
                    break;
            }

            var result = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Img = x.Img,
                Quantity = x.Quantity,
                Description = x.Description,
                NewPrice = x.NewPrice,
                OldPrice = x.OldPrice,
                QuantitySold = x.QuantitySold,
                CategoryId = x.CategoryId,
                SellerId = x.SellerId,
                RatingPoint = x.Reviews != null && x.Reviews.Any() ? x.Reviews.Select(r => r.Rating).Average() : 0,
                RatingCount = x.Reviews != null ? x.Reviews.Count : 0
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Product.GetProductDetail(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Name == name, "Category");
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetProductInCategory/{categoryId:int}")]
        public IActionResult GetProductInCategory(int categoryId)
        {
            var objList = _unitOfWork.Product.GetAll(u => u.CategoryId == categoryId, null, "Category");
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreateViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objToCreate = _mapper.Map<Product>(obj);
            _unitOfWork.Product.Add(objToCreate);
            _unitOfWork.Save();
            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _mapper.Map(obj, objFromDb);
            _unitOfWork.Product.Update(objFromDb);
            _unitOfWork.Save();
            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
