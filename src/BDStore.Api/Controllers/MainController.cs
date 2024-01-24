using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation.Results;

namespace BDStore.Api.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(result);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"MessagesHttp", Errors.ToArray()}
            }));
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddErrorToStack(error.ErrorMessage);
            }
            return CustomResponse();
        }
        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddErrorToStack(error.ErrorMessage);
            }

            return CustomResponse();
        }
        protected bool OperationValid()
        {
            return !Errors.Any();
        }
        protected void AddErrorToStack(string error)
        {
            Errors.Add(error);
        }
        protected void ClearErrors()
        {
            Errors.Clear();
        }
    }
}