using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.AllDishes = _context.Dishes.OrderByDescending(a => a.CreatedAt).ToList();
        return View();
    }
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("/add/process")]
    public IActionResult AddDish(Dish newDish)
    {
        //add to DB if data is correct 
        if(ModelState.IsValid)
        {
            //we can add to DB
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } else {
            // if not valid
            return View("Create");
        }
    }

    [HttpGet("delete/{DishId}")]
    public IActionResult DeleteDish(int DishId)
    {
        Dish dishToDelete = _context.Dishes.SingleOrDefault(a => a.DishId == DishId);
        _context.Dishes.Remove(dishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("/edit/{DishId}")]
    public IActionResult EditDish(int DishId)
    {
        Dish dishToEdit = _context.Dishes.FirstOrDefault(a => a.DishId == DishId);
        return View(dishToEdit);
    }

    [HttpPost("/edit/process/{DishId}")]
    public IActionResult UpdateDish(int DishId, Dish newVersionOfDish)
    {
        Dish oldDish = _context.Dishes.FirstOrDefault(a => a.DishId == DishId);
        oldDish.Name = newVersionOfDish.Name;
        oldDish.Chef = newVersionOfDish.Chef;
        oldDish.Tastiness = newVersionOfDish.Tastiness;
        oldDish.Calories = newVersionOfDish.Calories;
        oldDish.Description = newVersionOfDish.Description;
        oldDish.UpdatedAt = newVersionOfDish.UpdatedAt;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("/read/{DishId}")]
    public IActionResult ReadOne(int DishId)
    {
        ViewBag.OneDish = _context.Dishes.FirstOrDefault(a => a.DishId == DishId);
        return View("ReadOne");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
