using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Repository
{
    public class UsuarioRepository<TDbContext> : RepositoryBase<TDbContext>, IUsuarioRepository
    where TDbContext : DbContext, IFavoDeMelDbContext
    {
        protected DbSet<Usuario> UsuarioCrud => DbContext.Set<Usuario>();
        protected IQueryable<Usuario> UsuarioSelect => DbContext.Set<Usuario>().AsNoTracking();

        public UsuarioRepository(TDbContext dbContext) : base(dbContext)
        { }

        public async Task<bool> ExistsLoginAsync(string login)
        {
            return await UsuarioSelect.AnyAsync(c => c.Login == login);
        }

        public async Task<Usuario> LoginAsync(string login, string password)
        {
            return await UsuarioSelect.FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosPorPerfilAsync(UsuarioPerfil perfil)
        {
            return await UsuarioSelect.Where(c => c.Perfil == perfil && c.Ativo).ToListAsync();
        }

        public async Task<PagedList<Usuario>> ObterTodosPaginadoAsync(int page = 1, int pageSize = 20)
        {
            var pagedList = new PagedList<Usuario>();

            var itensPaginado = await UsuarioSelect.PageBy(x => x.Id, page, pageSize).ToListAsync();
            var total = await UsuarioSelect.CountAsync();

            pagedList.Data.AddRange(itensPaginado);
            pagedList.TotalCount = total;
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public async Task<bool> UsuarioSenhaValidoAsync(string login, string password)
        {
            return await UsuarioSelect.AnyAsync(c => c.Login == login && c.Password == password);
        }

        public async Task<Usuario> EditarAsync(Usuario usuario)
        {
            UsuarioCrud.Update(usuario);
            return await Task.Run(() => usuario);
        }

        public async Task<Usuario> CadastrarAsync(Usuario usuario)
        {
            var usuarioDb = await UsuarioCrud.AddAsync(usuario);
            usuario.Id = usuarioDb.Entity.Id;
            return usuario;
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await UsuarioSelect.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
