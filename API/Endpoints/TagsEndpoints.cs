using API.Extensions;
using Application.Features.Tags;
using Application.Features.Tags.Commands.RenameTag;
using Application.Features.Tags.Queries.GetAll;
using Application.Features.Tags.Queries.GetById;
using MediatR;


namespace API.Endpoints
{
    public static class TagsEndpoints
    {
       public static void MapTagsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/tags");

            app.MapGet("", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllTagsQuery());

                return result.ToHttpResult<IReadOnlyList<TagDto>>();
            });

            app.MapPost("{id}", async ( RenameTagCommand command,ISender sender) =>
            {
               var result= await sender.Send(command);

                return result.ToHttpResult();
            });

            app.MapGet("{id}", async (Guid id,ISender sender) =>
            {
                var result = await sender.Send(new GetTagByIdQuery(id));

                return result.ToHttpResult<TagDto>();
            });
        } 
    }
}
