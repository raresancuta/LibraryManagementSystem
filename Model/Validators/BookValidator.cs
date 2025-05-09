using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Validators
{
    public class BookValidator : Validator<Book>
    {
        public BookValidator() { }
        public void Validate(Book entity)
        {
            string errors = "";
            if (string.IsNullOrEmpty(entity.Title))
            {
                errors += "Title cannot not be null\n";
            }
            if (string.IsNullOrEmpty(entity.Author))
            {
                errors += "Author cannot be null\n";
            }
            if (string.IsNullOrEmpty(entity.ISBN))
            {
                errors += "ISBN cannot be null\n";
            }
            if(entity.NoOfAvailableCopies < 0)
            {
                errors += "No. of copies cannot be negative";
            }
            if (entity.Quantity < 0)
            {
                errors += "Official quantity cannot be negative";
            }
            if (entity.NoOfAvailableCopies > entity.Quantity)
            {
                errors += "No. of copies cannot be greater that official quantity\n";
            }
            if (errors.Length > 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
