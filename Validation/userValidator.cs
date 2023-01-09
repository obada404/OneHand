using FluentValidation;
using OneHandTraining.controller;
using OneHandTraining.model;
using OneHandTraining.DTO;

namespace OneHandTraining.Validation;

public class userValidator : AbstractValidator<UserRegistrationRequest>
{
    public userValidator()
    {
        RuleFor(x =>x.Email).NotEmpty().EmailAddress();
        RuleFor(x =>x.Password).NotEmpty();
        RuleFor(x =>x.Username).NotEmpty();
    }
}