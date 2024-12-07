namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerConfirmadoDomainEvent(Guid AlquierlerId) : IDomainEvent;