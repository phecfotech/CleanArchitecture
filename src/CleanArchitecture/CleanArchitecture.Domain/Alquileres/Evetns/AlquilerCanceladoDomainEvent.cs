namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerCanceladoDomainEvent(Guid AlquierlerId) : IDomainEvent;