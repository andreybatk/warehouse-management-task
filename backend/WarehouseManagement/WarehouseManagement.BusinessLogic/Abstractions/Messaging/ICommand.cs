using MediatR;

namespace WarehouseManagement.BusinessLogic.Abstractions.Messaging;
public interface ICommand<out TResponse> : IRequest<TResponse>;
