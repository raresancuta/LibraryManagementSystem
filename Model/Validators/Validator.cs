using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Model.Validators
{
    public interface Validator<E>
    {
        void Validate(E entity);
    }
}
