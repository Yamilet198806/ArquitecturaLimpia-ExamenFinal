//Crear configuración
using ArquitecturaLimpia.Infraestructura;
using ArquitecturaLimpiaApp.Dominio.Entidades;
using ArquitecturaLimpiaAPP.Infraestructura.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuracion = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var opciones = new DbContextOptionsBuilder<AppDbContext>();
opciones.UseSqlServer(configuracion.GetConnectionString("ConexionDB"));

using (var contexto = new AppDbContext(opciones.Options))
{
   
    await contexto.Database.EnsureCreatedAsync();

    using (var unidadDeTrabajo = new UnidadDeTrabajo(contexto))
    {
        Console.WriteLine("Ingrese los datos del usuario:");

        var usuario = new Usuario
        {
            Nombre = ObtenerEntradaTeclado("Nombre"),
            CorreoElectronico = ObtenerEntradaTeclado("Correo Electronico"),
            Contraseña = ObtenerEntradaTeclado("Contraseña")
        };

        await unidadDeTrabajo.Usuarios.CrearAsync(usuario);
        await unidadDeTrabajo.GrabarCambiosAsync();
        Console.WriteLine("Usuario grabado correctamente en la Base de Datos");

        //Lista usuarios registrados
        var usuarios = await unidadDeTrabajo.Usuarios.ObtenerTodosAsync();
        Console.WriteLine("Lista de Usuarios:");
        foreach (var osuario in usuarios)
        {
            Console.WriteLine($"ID:{osuario.Id},Nombre:{osuario.Nombre},Email:{osuario.CorreoElectronico}");
        }
    }
}

string ObtenerEntradaTeclado(string mensaje)
{
    Console.Write($"{mensaje}: ");
    return Console.ReadLine();
}