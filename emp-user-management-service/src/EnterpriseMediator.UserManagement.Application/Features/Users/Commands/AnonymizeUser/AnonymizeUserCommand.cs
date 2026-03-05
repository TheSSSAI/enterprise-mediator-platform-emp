using MediatR;
using System;

namespace EnterpriseMediator.UserManagement.Application.Features.Users.Commands.AnonymizeUser;

/// <summary>
/// Command to anonymize a user's personal identifiable information (PII) for GDPR compliance ("Right to be Forgotten").
/// This will scramble the user's name, email, and other sensitive data, and deactivate the account.
/// </summary>
/// <param name="UserId">The unique identifier of the user to anonymize.</param>
/// <param name="Reason">The reason for anonymization (e.g., "GDPR Request").</param>
public sealed record AnonymizeUserCommand(Guid UserId, string Reason) : IRequest<bool>;