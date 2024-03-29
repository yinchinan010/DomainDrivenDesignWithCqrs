﻿using DomainDrivenDesignWithCqrs.AppLayer.Services;
using DomainDrivenDesignWithCqrs.Contracts;
using MediatR;

namespace DomainDrivenDesignWithCqrs.AppLayer.RequestHandlers;

internal class RequestValidatorMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	where TResponse : ResponseBase, new()
{
	private readonly IValidationService ValidationService;

	public RequestValidatorMiddleware(IValidationService validationService)
	{
		ValidationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
	}

	public async Task<TResponse> Handle(
		TRequest request,
		CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		IEnumerable<ValidationError> errors =
			await ValidationService.ValidateAsync(request, mustHaveAValidator: true);
		if (errors.Any())
			return new TResponse
			{
				Status = ResponseStatus.BadRequest,
				ValidationErrors = errors
			};
		return await next();
	}
}