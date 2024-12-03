using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres;

public sealed class Alquiler: Entity
{
    public Alquiler(GUid id): base(id)
    {

    }

    public AlquilerStatus Status (get; private set;)
}