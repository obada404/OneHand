using FluentValidation;
using OneHandTraining.controller;

namespace OneHandTraining.Validation;

public class userValidator : AbstractValidator<UserRequest>
{
    public userValidator()
    {
        RuleFor(x =>x.Email).NotEmpty().EmailAddress();
        RuleFor(x =>x.Password).NotEmpty();
        RuleFor(x =>x.Username).NotEmpty();
    }
}