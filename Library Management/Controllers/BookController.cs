using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var books = BookService.Instance.GetBooks();
            return View(books);
        }

        public IActionResult Add()
        {
            // This shows the Add.cshtml form
            return View(new AddBookViewModel());
        }

        // POST: Book/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Re-render the form with validation errors
                return View(vm);
            }

            BookService.Instance.AddBook(vm);

            // After saving, go back to book list
            return RedirectToAction("Index");
        }


        public IActionResult EditModal(Guid id)
        {
            var editBookViewModel = BookService.Instance.GetBookById(id);
            if (editBookViewModel == null) return NotFound();

          
            return PartialView("_EditBookPartial", editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // If model state is not valid, you can return a view with validation errors
                return BadRequest(ModelState);
            }

            // Assuming BookService has a method to update the book
            BookService.Instance.UpdateBook(vm);

            return Ok();
        }

        public IActionResult DeleteModal(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null) return NotFound();

            return PartialView("_DeleteBookPartial", book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid BookId)
        {
            BookService.Instance.DeleteBook(BookId);
            return RedirectToAction("Index");
        }



        public IActionResult Details(Guid id)
        {
            var book = BookService.Instance.GetBooks().First(b => b.BookId == id);
            return View(book);
        }


    }
}
