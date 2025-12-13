using API.Extensions;
using Application.Features.Tags;
using Application.Features.Tags.Commands.CreateTag;
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
            var group = app.MapGroup("/api/tags").WithTags("Tags");

            group.MapGet("", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllTagsQuery());

                return result.ToHttpResult<IReadOnlyList<TagDto>>();
            });

            group.MapGet("{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetTagByIdQuery(id));

                return result.ToHttpResult<TagDto>();
            });

            group.MapPatch("{id}", async ( RenameTagCommand command,ISender sender) =>
            {
               var result= await sender.Send(command);

                return result.ToHttpResult();
            });

            group.MapPost("", async (CreateTagCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

               return result.ToHttpResult<Guid>();

            });

        } 
    }
}
