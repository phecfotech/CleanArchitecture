namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerCompletadoDomainEvent(Guid AlquierlerId) : IDomainEvent;