using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IRestaurantRepository restaurantRepo,
        IOrderRepository orderRepo,UserManager<User> userManager
        , IUnitOfWork unitOfWork,IOrderNotificationService notificationService)
        : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            
           var user= await userManager.FindByIdAsync(command.UserId.ToString());
              if(user is null)
                return Result<Guid>.Failure(AuthErrors.InvalidCredential);


            var restaurant = await restaurantRepo.GetRestaurantWithListMenuItemsByIdAsync(command.RestaurantId, cancellationToken);

          if(restaurant is null)
              return Result<Guid>.Failure(RestaurantErrors.NotFound(command.RestaurantId));

            var OrderItemsDtos = command.OrderItems;

            var ids= OrderItemsDtos.Select(oi => oi.MenuItemId).ToList();

           
            var MenuItems = restaurant.MenuItems.Where(mi => ids.Contains(mi.Id)).ToList();

            foreach (var orderItem in OrderItemsDtos)
            {
               var menuItem= MenuItems.FirstOrDefault(x => x.Id == orderItem.MenuItemId);

                if (orderItem.UnitPrice!= menuItem.Price)
                    return Result<Guid>.Failure(OrderErrors.InvalidPrice(orderItem.MenuItemId));

            }
            var OrderItems = command.OrderItems.Select(o => o.ToOrderItem()).ToList();

            var order = new Domain.Entities.Order
            {
                CustomerId= command.UserId,
                Status= OrderStatus.Placed,
                OrderItems= OrderItems,
                OrderPrice= OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
                RestaurantId= command.RestaurantId,
                CreatedAt= DateTime.UtcNow
            };
            
            orderRepo.Add(order);

           var isPersisted= await unitOfWork.SaveChangesAsync(cancellationToken);

            if(!isPersisted)
                return Result<Guid>.Failure(Error.Persistance);

            await notificationService.OrderCreatedAsync(order);

            return Result<Guid>.Success(order.Id);
        }
    }
}
