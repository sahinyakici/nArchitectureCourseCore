using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehaviour(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        List<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (userRoleClaims == null)
        {
            throw new AuthorizationException("You are not authenticated");
        }

        bool isNotMathcedAUserRoleClaimWithRequestRoles = userRoleClaims.FirstOrDefault(userRoleClaim =>
                userRoleClaim == GeneralOperationClaims.Admin || request.Roles.Any(role => role == userRoleClaim))
            .IsNullOrEmpty();

        if (isNotMathcedAUserRoleClaimWithRequestRoles)
        {
            throw new AuthorizationException("You are not authorized");
        }

        TResponse response = await next();
        return response;
    }
}