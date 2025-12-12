using Domain.Abstractions.Result;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;


namespace API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult(this Result result)
        {
            if (result.IsSuccess)
                return Results.Ok();

            return GetErrorResponse(result);

        }

        public static IResult ToHttpResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return Results.Ok(result.Value);

            return GetErrorResponse(result);


        }

        private static IResult GetErrorResponse(Result result)
        {
        return  result.Error.Code switch
               {
                ErrorCodes.NotFound => Results.NotFound(result.Error.Message),
                ErrorCodes.BadRequest => Results.BadRequest(result.Error.Message),
                ErrorCodes.Presistance => Results.BadRequest(result.Error.Message),
                ErrorCodes.Validation => Results.BadRequest(GetProplemDetails(result.Error)),
                ErrorCodes.Forbidden => Results.Forbid(),
                _ => Results.StatusCode(500)
                };
        }

        private static ProblemDetails GetProplemDetails(Error error)
        {
            var proplemDetails = new ProblemDetails
            {
                Title = error.Code,
                Status = 400,
                Detail = error.Message
            };

            return proplemDetails;
        }
    }
}
