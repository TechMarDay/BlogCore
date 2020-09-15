using FluentValidation;
using System;

namespace Web.Controllers
{
    public class BaseValidation<Tval, Tcom> where Tval : AbstractValidator<Tcom>
    {
        public BaseValidation()
        {
            Activator.CreateInstance(typeof(Tval));
        }
    }
}