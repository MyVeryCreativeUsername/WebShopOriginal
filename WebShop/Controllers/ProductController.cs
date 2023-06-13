using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebShop.Data;
using WebShop.DTOs.ShopDTOs;
using WebShop.Models.ShopEntities;
using WebShop.Models.UserEntities;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ShopDb _dbHandle;
        public ProductController(ShopDb dbHandle) {_dbHandle = dbHandle;}




        [HttpPost]
        [Route("ReviewProduct")]
        public ActionResult ReviewProduct(ReviewDTO reviewDto)
        {

            var review = new Review();

            review.UserId = reviewDto.UserId;
            review.ProductId = reviewDto.ProductId;
            review.Comment = reviewDto.Comment;
            review.Rating = reviewDto.Rating;


            _dbHandle.Reviews.Add(review);
            _dbHandle.SaveChanges();

            return Ok(_dbHandle.Reviews);
        }



        [HttpPost]
        [Route("ShowReviewProduct")]
        public ActionResult ShowReviewedProduct(int id)
        {
            var reviewedProduct = _dbHandle.Products
                .Include(x => x.ProductReviews)
                .Where(x => x.Id == id).ToList();

            return Ok(reviewedProduct);
        }




        [HttpPost]
        [Route("DeleteReviewProduct")]
        public ActionResult DeleteReviewedProduct(RemoveReviewDTO RemoveReviewDTO)
        {
            var deleteReview = _dbHandle.Reviews.FirstOrDefault(x => x.Id == RemoveReviewDTO.ReviewId && x.ProductId == RemoveReviewDTO.ProductId);

            _dbHandle.Reviews.Remove(deleteReview);

            _dbHandle.SaveChanges();

            return Ok(_dbHandle.Reviews);
        }



        [HttpPost]
        [Route("AddDiscountProduct")]
        public ActionResult AddDiscountProduct(DiscountDTO discountDto)
        {
            var discount = new Discount();

            discount.ProductId = discountDto.ProductId;
            discount.Description = discountDto.Description;
            discount.NewPrice = discountDto.NewPrice;

            _dbHandle.Add(discount);
            _dbHandle.SaveChanges();

            return Ok(_dbHandle.Discounts);
        }



        [HttpPost]
        [Route("ShowDiscountProduct")]
        public ActionResult ShowDiscountProduct(int id)
        {
            var discountedProduct = _dbHandle.Products
                .Include(x => x.ProductDiscount)
                .Where(x => x.Id == id).ToList();


            return Ok(discountedProduct);
        }




        [HttpPost]
        [Route("RemoveDiscountProduct")]
        public ActionResult RemoveDiscountProduct(int id)
        {
            var itemToRemove = _dbHandle.Discounts.SingleOrDefault(x => x.ProductId == id); //returns a single item.

            if (itemToRemove != null)
            {
                _dbHandle.Discounts.Remove(itemToRemove);
                _dbHandle.SaveChanges();
            }

            return Ok("Discound Removed!");
        }




        [HttpPost]
        [Route("SeedCategory")]
        public ActionResult SeedCategory()
        {
            Random random = new Random();

            var category = new Faker<Category>().RuleFor(s => s.CategoryName, f => f.Commerce.Department());

            var categories = category.Generate(10);

            _dbHandle.Categorys.AddRange(categories);

            _dbHandle.SaveChanges();

            return Ok(_dbHandle.Categorys);
        }




        [HttpPost]
        [Route("SeedProducts")]
        public ActionResult SeedProducts()
        {
            var categories = _dbHandle.Categorys.ToList();
            
            Random random = new Random();

            var product = new Faker<Product>()
                .RuleFor(s => s.Name, f => f.Commerce.ProductName())
                .RuleFor(s => s.Price, f => f.Random.Float(1, 200))
                .RuleFor(s => s.Categorys, f => categories.OrderBy(x => random.Next()).Take(random.Next(1, 6)).ToList())
                .RuleFor(s => s.Qty, f => f.Random.Int(1, 500));


            var products = product.Generate(300);

            _dbHandle.Products.AddRange(products);

            _dbHandle.SaveChanges();

            return Ok(_dbHandle.Products);
        }


    }
}
