using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Car.BLLayer.DTO.RequestDtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

    public class ChangePasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }

    //update
    public class UpdateRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }
        [EmailAddress]
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
    }

    public class UpdateRequestValidator : AbstractValidator<UpdateRequestDto>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(3, 50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }


    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var value in context.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                context.Result = new BadRequestObjectResult(new { message = "Validation errors", errors });
            }
        }
    }
}
