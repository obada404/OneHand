using FluentValidation;
using OneHandTraining.controller;
namespace OneHandTraining.Validation;

public class ArticleValidator : AbstractValidator<ArticleRequest>
{

    public ArticleValidator()
    {

        RuleFor(x => x.body).NotEmpty().MaximumLength(400);
        RuleFor(x => x.title).NotEmpty().MaximumLength(40);
        RuleFor(x => x.description).NotEmpty().MaximumLength(100);


    }
    
}