namespace ClearArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerReservadoDomainEvent(Guid AlquierId): IDomainEvent;