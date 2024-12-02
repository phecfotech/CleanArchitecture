using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity
{
    public User(Guid id, Nombre nombre, Apellido apellido, Email email) : base(id)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;

    }

    public Nombre? nombre { get; private set; }
    public Apellido? apellido { get; private set; }
    public Email email { get; private set; }
    public static User create(
        Nombre nombre,
        Apellido apellido,
        Email email
    )
    {
        var user = new User(Guid.NewGuid(), nombre, apellido, email);
        return user;
    }

}