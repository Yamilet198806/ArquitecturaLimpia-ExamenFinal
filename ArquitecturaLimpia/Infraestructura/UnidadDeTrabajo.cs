using ArquitecturaLimpiaApp.Dominio.Interfaces;
using ArquitecturaLimpiaApp.Infraestructura.Repositorios;
using ArquitecturaLimpiaAPP.Dominio.Interfaces;
using ArquitecturaLimpiaAPP.Infraestructura.Contexto;

namespace ArquitecturaLimpia.Infraestructura
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly AppDbContext _dbContext;
        private IUsuarioRepositorio _usuarioRepositorio;

        public UnidadDeTrabajo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IUsuarioRepositorio Usuarios
        {
            get { 
                return _usuarioRepositorio ??= new UsuarioRepositorio(_dbContext); 
            }
        }
        public async Task<int> GrabarCambiosAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
