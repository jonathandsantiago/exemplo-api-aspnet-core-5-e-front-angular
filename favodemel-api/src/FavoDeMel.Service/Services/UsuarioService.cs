using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Dtos.Mappers;
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FavoDeMel.Domain.Dtos.Mappers.UsuarioMappers;

namespace FavoDeMel.Service.Services
{
    public class UsuarioService : ServiceCacheBase, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IGeradorGuidService _geradorGuidService;
        private readonly UsuarioValidator _validator;
        public IList<string> MensagensValidacao => _validator?.Mensagens ?? new List<string>();

        public UsuarioService(IServiceCache serviceCache,
            IUsuarioRepository repository,
            IGeradorGuidService geradorGuidService,
            UsuarioValidator validator) : base(serviceCache)
        {
            _repository = repository;
            _geradorGuidService = geradorGuidService;
            _validator = validator;
        }

        public async Task<UsuarioDto> Login(LoginDto loginDto)
        {
            if (!await _validator.ValidarLogin(loginDto))
            {
                return null;
            }

            var usuario = await _repository.Login(loginDto.Login, loginDto.PasswordHash);
            UsuarioDto dto = usuario.ToDto();
            await SalvarCache(dto.Id, dto);
            return dto;
        }

        public async Task<UsuarioDto> ObterPorId(Guid id)
        {
            var usuarioCache = await ObterPorIdInCache<UsuarioDto, Guid>(id);

            if (usuarioCache != null)
            {
                return usuarioCache;
            }

            var usuario = await _repository.ObterPorId(id);
            return usuario?.ToDto();
        }

        public async Task<UsuarioDto> Inserir(UsuarioDto usuarioDto)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validator))
            {
                if (!await _validator.Validar(usuarioDto))
                {
                    return null;
                }

                Usuario usuario = usuarioDto.ToEntity();
                usuario.Id = _geradorGuidService.GetNexGuid();
                usuario.Prepare();
                Usuario usuarioDb = await _repository.Inserir(usuario);
                UsuarioDto dto = usuarioDb.ToDto();
                await SalvarCache(dto.Id, dto);
                return dto;
            }
        }

        public async Task<UsuarioDto> Editar(UsuarioDto usuarioDto)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validator))
            {
                if (!await _validator.Validar(usuarioDto))
                {
                    return null;
                }

                Usuario usuario = usuarioDto.ToEntity();
                usuario.Prepare();

                Usuario usuarioDb = await _repository.ObterPorId(usuario.Id);
                usuario.Login = usuarioDb.Login;
                usuario.Password = usuarioDb.Password;

                await _repository.Editar(usuario);
                UsuarioDto dto = usuarioDb.ToDto();
                await SalvarCache(dto.Id, dto);
                return dto;
            }
        }

        public async Task<bool> AlterarSenha(Guid id, string password)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validator))
            {
                Usuario usuario = await _repository.ObterPorId(id);

                if (!_validator.PermiteEditarSenha(usuario.Password, password))
                {
                    return false;
                }

                usuario.Password = StringHelper.CalculateMD5Hash(password);
                await _repository.Editar(usuario);
                UsuarioDto dto = usuario.ToDto();
                await SalvarCache(dto.Id, dto);
                return true;
            }
        }

        public async Task<PaginacaoDto<UsuarioDto>> ObterTodosPaginado(FiltroUsuario filtro)
        {

            var produtos = await _repository.ObterTodosPaginado(filtro.Pagina, filtro.Limite);
            return produtos?.ToPaginacaoDto<PaginacaoDto<UsuarioDto>>();
        }

        public async Task<IEnumerable<UsuarioDto>> ObterTodosPorPerfil(UsuarioPerfil perfil)
        {
            var usuarios = await _repository.ObterTodosPorPerfil(perfil);
            return usuarios?.ToListDto();
        }
    }
}