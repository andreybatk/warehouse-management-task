using MediatR;

namespace WarehouseManagement.BusinessLogic.Abstractions.Messaging;
public interface IQuery<out TResponse> : IRequest<TResponse>;