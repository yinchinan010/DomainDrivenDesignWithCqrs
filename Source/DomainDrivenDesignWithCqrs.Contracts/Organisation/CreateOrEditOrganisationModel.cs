﻿using FluentValidation;

namespace DomainDrivenDesignWithCqrs.Contracts.Organisation;

public class CreateOrEditOrganisationModel
{
	public string? Name { get; set; }

	internal class Validator : AbstractValidator<CreateOrEditOrganisationModel>
	{
		public Validator()
		{
			RuleFor(x => x.Name).NotEmpty();
		}
	}
}
