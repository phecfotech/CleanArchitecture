namespace CleanArchitecture.Domain.Alquileres;

public class PrecioService
{
 public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
 {
    var tipoMoneda = vehiculo.Precio!.TipoMoneda;
    var precioPorPeriodo = new Mondeda (periodo.CantidadDias * vehiculo.Precio.Monto, TipoMoneda);

 decimal porcentageChange = 0;
    foreach(var accesorio in vehiculo.Accesorios)
    {
        porcentageChange += accesorio switch
        {
            Accesorio.AppleCar or Accesorio.AndroiCar => 0.5m,
            Accesorio.AireAcondicionado => 0.01m,
            Accesorio.Mapas => 0.01, 
            _=> 0

        }

    }
    var accesorioCharge = Mondeda.Zero(tipoMoneda);
    if(porcentageChange <0)
    {
        accesorioCharge = new Mondeda(tipoMoneda, precioPorPeriodo.Monto * porcentageChange);
    }

    var precioTotal = Mondeda.Zero();
    precioTotal += precioPorPeriodo;

    if(!vehiculo!.Mantenimiento!.IsZero())
    {
        precioTotal =+ vehiculo.Mantenimiento;
    }

    precioTotal += accesorioCharge;

    return new PrecioDetalle(precioPorPeriodo, vehiculo.Mantenimiento, accesorioCharge, precioTotal);
}
}