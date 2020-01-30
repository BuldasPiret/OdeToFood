using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        private OdeToFoodDb db = new OdeToFoodDb();
        public ActionResult BestReview()
        {
            var best = from r in _reviews
                       orderby r.rating descending
                       select r;
            return PartialView("_review", best.First());
        }
        // GET: Reviews

        public ActionResult LatestReviews()
        {
            var model = from r in _reviews
                        orderby r.country
                        select r;
            return View(model);
        }

        public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
        {
            var restaurant = db.Restaurants.Find(restaurantId);
            if (restaurant != null)
            {
                return View(restaurant);
            }
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Reviews/Create
        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            var model = db.Restaurants.Find(restaurantId);
            ViewBag.Name = model.Name;
            ViewBag.restaurantId = model.Id;
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Reviews.Find(id);
            return View(model);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "RevierName")] RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                var editable_review = db.Reviews.Find(review.id);
                editable_review.Body = review.Body;
                editable_review.rating = review.rating;
                db.Entry(editable_review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = editable_review.RestaurantId });
            }
            return View(review);
        }

        static List<RestaurantReview> _reviews = new List<RestaurantReview>
        {
            new RestaurantReview
            {
                id = 1,
                name = "Cinnamon Club",
                city = "London",
                country = "UK",
                rating = 10,
            },
            new RestaurantReview
            {
                id = 2,
                name = "Marrakesh",
                city = "D.C",
                country = "USA",
                rating = 10,
            },
            new RestaurantReview
            {
                id = 3,
                name = "The House of Elliot",
                city = "Ghent",
                country = "Belgium",
                rating = 10,
            },
        };
    }
}
