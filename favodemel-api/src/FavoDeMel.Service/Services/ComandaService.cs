using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Entities.Comandas;
using FavoDeMel.Domain.Entities.Comandas.Commands;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FavoDeMel.Domain.Dtos.Mappers.ComandaMappers;

namespace FavoDeMel.Service.Services
{
    public class ComandaService : IComandaService
    {
        private readonly IServiceCache _serviceCache;
        private readonly IComandaRepository _repository;
        private readonly IGeradorGuidService _geradorGuidService;
        private readonly IMensageriaService _mensageriaService;
        private readonly ComandaValidator _validador;
        public IList<string> MensagensValidacao => _validador?.Mensagens ?? new List<string>();

        public ComandaService(IServiceCache serviceCache,
            IComandaRepository repository,
            IGeradorGuidService geradorGuidService,
            IMensageriaService mensageriaService,
            ComandaValidator comandaValidator)
        {
            _serviceCache = serviceCache;
            _repository = repository;
            _validador = comandaValidator;
            _mensageriaService = mensageriaService;
            _geradorGuidService = geradorGuidService;
        }

        public async Task<ComandaDto> ObterPorIdAsync(Guid id)
        {
            var comandoCache = await _serviceCache.ObterAsync<ComandaDto, Guid>(id);

            if (comandoCache != null)
            {
                return comandoCache;
            }

            var comanda = await _repository.ObterPorIdAsync(id);
            var dto = comanda?.ToDto();
            if (dto != null)
            {
                await _serviceCache.SalvarAsync(dto.Id, dto);
            }
            return dto;
        }

        public async Task<ComandaDto> CadastrarAsync(ComandaDto comandaDto)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!await _validador.ValidarAsync(comandaDto))
            {
                return null;
            }

            Comanda comanda = comandaDto.ToEntity();
            comanda.Id = _geradorGuidService.GetNexGuid();

            foreach (var pedido in comanda.Pedidos)
            {
                pedido.Id = _geradorGuidService.GetNexGuid();
            }

            comanda.Prepare();
            comanda.DataCadastro = DateTime.Now;
            Comanda comandaDb = await _repository.CadastrarAsync(comanda);
            ComandaDto dto = comandaDb.ToDto();
            await _serviceCache.SalvarAsync(dto.Id, dto);
            await _mensageriaService.EnviarAsync(new ComandaCadastroCommand(dto));
            return dto;
        }

        public async Task<ComandaDto> EditarAsync(ComandaDto comandaDto)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!await _validador.ValidarAsync(comandaDto))
            {
                return null;
            }

            Comanda comanda = comandaDto.ToEntity();
            comanda.Prepare();
            await _repository.EditarAsync(comanda);
            ComandaDto dto = comanda.ToDto();
            await _serviceCache.SalvarAsync(dto.Id, dto);
            await _mensageriaService.EnviarAsync(new ComandaEditarCommand(dto));
            return dto;
        }

        public async Task<IEnumerable<ComandaDto>> ObterTodosPorSituacaoAsync(ComandaSituacao situacao)
        {
            var comandas = await _repository.ObterTodosPorSituacaoAsync(situacao);
            return comandas?.ToListDto();
        }

        public async Task<PaginacaoDto<ComandaDto>> ObterPaginadoPorSituacaoAsync(FiltroComanda filtroComanda)
        {
            var comandas = await _repository.ObterPaginadoPorSituacaoAsync(filtroComanda.Situacao, filtroComanda.Data, filtroComanda.Pagina, filtroComanda.Limite);
            return comandas?.ToPaginacaoDto<PaginacaoDto<ComandaDto>>();
        }

        public async Task<ComandaDto> ConfirmarAsync(Guid comandaId)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!_validador.PermiteAlterarSituacao(comandaId))
            {
                return null;
            }

            Comanda comanda = await _repository.ConfirmarAsync(comandaId);
            ComandaDto dto = comanda.ToDto();
            await _serviceCache.SalvarAsync(dto.Id, dto);
            await _mensageriaService.EnviarAsync(new ComandaConfirmarCommand(dto));
            return dto;
        }

        public async Task<ComandaDto> Fechar(Guid comandaId)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!_validador.PermiteAlterarSituacao(comandaId))
            {
                return null;
            }

            Comanda comanda = await _repository.FecharAsync(comandaId);
            ComandaDto dto = comanda.ToDto();
            await _serviceCache.SalvarAsync(dto.Id, dto);
            await _mensageriaService.EnviarAsync(new ComandaFecharCommand(dto));
            return comanda?.ToDto();
        }
    }
}