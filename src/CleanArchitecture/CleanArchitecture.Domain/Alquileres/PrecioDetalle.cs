namespace CleanArchitecture.Domain.Alquileres;

public record PrecioDetalle (Mondeda PrecioPorPeriodo, 
Mondeda Mantenimiento, Mondeda Accesorios, Mondeda PrecioTotal);