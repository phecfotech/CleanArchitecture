using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;

namespace CleanArchitecture.Domain.Alquileres;

public sealed class Alquiler : Entity
{
    private Alquiler(GUid id, Guid vehiculoId, Guid userId, DateRange duracion, Mondeda precioPorPeriodo, Moneda mantenimiento, Mondeda accesorios, Mondeda precioTotal, AlquilerStatus status, DateTime fechaCreacion) : base(id)
    {
        VehiculoId = vehiculoId;
        UserId = userId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPeriodo;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }
    public Guid VehiculoId { get; private set; }
    public Guid UserId { get; private set; }
    public Moneda? PrecioPorPeriodo { get; private set; }
    public Moneda? Mantenimiento { get; private set; }
    public Moneda? Accesorios { get; private set; }
    public Moneda? PrecioTotal { get; private set; }

    public AlquilerStatus Status { get; private set; }
    public DateRange? Duracion { get; private set; }
    public DateTime? FechaCreacion { get; private set; }
    public DateTime? FechaConfirmacion { get; private set; }
    public DateTime? FechaDenagacion { get; private set; }
    public DateTime? FechaCompletado { get; private set; }
    public DateTime? FechaCanelacion { get; private set; }

    public static Alquiler Reservar(
        Guid vehiculoId, Guid userId, DateRange duracion, DateTime fechaCreacion, PrecioService precioService
    )
    {
var  precioDetalle = precioService.CalcularPrecio(vehiculo, duracion);
        var alquiler = new Alquiler(Guid.NewGuid(),
            vehiculoId, userId, duracion, fechaCreacion, precioDetalle.PrecioPorPeriodo, precioDetalle.Mantenimiento,
            precioDetalle.Accesorios,
            AlquilerStatus.Reservado);

        alquiler.RaiseDomainEvent( new AlquilerReservadoDomainEvent(alquiler))

        vehiculo.fechaUltimoAlquiler = fechaCreacion;
            return alquiler;
    }

    public Result Confirmar (DateTime utcNow)
    {
        if(Status !=AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);
             
        }

        Status = AlquierStatus.Confirmado;
        FechaConfirmacion = utcNow;

        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(Id));

        return Result.Success();
    }


    public Result Rechazar(DateTime utcNow)
    {

        if(Status != AlquierStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);

        }

        Status = AlquierStatus.Rechazado;
        FechaDenagacion = utcNow;
        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id));

        return Result.Sucess();
    }

    public Result Cancelar(DateTime utcNow)
    {

        if(Status != AlquierStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmado);

        }

        var currentDate = DateOnly.FromDateTime(utcNow);
        if(currentDate > Duracion!.Inicio)
        {
            return Resutl.Failure(AlquilerErrors.AlreadyStared);
        }

        Status = AlquierStatus.Cancelado;
        FechaCanelacion = utcNow;
        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id));

        return Result.Sucess();
    }
}