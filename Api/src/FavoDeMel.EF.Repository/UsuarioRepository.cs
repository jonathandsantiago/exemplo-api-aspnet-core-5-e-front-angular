using FavoDeMel.Domain.Usuarios;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository
{
    public class UsuarioRepository : RepositoryBase<int, Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(BaseDbContext dbContext) : base(dbContext)
        { }

        public async Task<bool> ExistsLogin(string login)
        {
            return await _dbSet.AnyAsync(c => c.Login == login);
        }

        public async Task<Usuario> Login(string login, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosPorPerfil(UsuarioPerfil perfil)
        {
            return await _dbSet.Where(c => c.Perfil == perfil && c.Ativo).ToListAsync();
        }
    }
}
