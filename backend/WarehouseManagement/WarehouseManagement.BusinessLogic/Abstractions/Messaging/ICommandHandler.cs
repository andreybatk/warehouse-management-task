using MediatR;

namespace WarehouseManagement.BusinessLogic.Abstractions.Messaging;
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;
