using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Validators
{
    public class LendingValidator:Validator<Lending>
    {
        public LendingValidator() { }

        public void Validate(Lending entity)
        {
            string errors = "";
            if (string.IsNullOrEmpty(entity.ClientName))
            {
                errors += "The name of the client cannot be null\n";
            }
            if(entity.Book.NoOfAvailableCopies < 1)
            {
                errors += "There are not enough books\n";
            }
            if (errors.Length > 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
