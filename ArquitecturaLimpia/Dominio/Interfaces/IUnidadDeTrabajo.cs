using ArquitecturaLimpiaApp.Dominio.Interfaces;

namespace ArquitecturaLimpiaAPP.Dominio.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuarioRepositorio Usuarios { get; }
        Task<int> GrabarCambiosAsync();
    }
}
