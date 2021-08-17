using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
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
    public class ComandaService : ServiceCacheBase, IComandaService
    {
        private readonly IComandaRepository _repository;
        private readonly IGeradorGuidService _geradorGuidService;
        private readonly IMensageriaService _mensageriaService;
        private readonly ComandaValidator _validador;
        public IList<string> MensagensValidacao => _validador?.Mensagens ?? new List<string>();

        public ComandaService(IServiceCache serviceCache,
            IComandaRepository repository,
            IGeradorGuidService geradorGuidService,
            IMensageriaService mensageriaService,
            ComandaValidator comandaValidator) : base(serviceCache)
        {
            _repository = repository;
            _validador = comandaValidator;
            _mensageriaService = mensageriaService;
            _geradorGuidService = geradorGuidService;
        }

        public async Task<ComandaDto> ObterPorIdAsync(Guid id)
        {
            var comandoCache = await ObterPorIdInCache<ComandaDto, Guid>(id);

            if (comandoCache != null)
            {
                return comandoCache;
            }

            var comanda = await _repository.ObterPorIdAsync(id);
            ComandaDto dto = comanda?.ToDto();
            if (dto != null)
            {
                await SalvarCache(dto.Id, dto);
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
            await SalvarCache(dto.Id, dto);
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
            await SalvarCache(dto.Id, dto);
            await _mensageriaService.EnviarAsync(new ComandaEditarCommand(dto));
            return dto;
        }

        public async Task<IEnumerable<ComandaDto>> ObterTodosPorSituacaoAsync(ComandaSituacao situacao)
        {
            var comandas = await _repository.ObterTodosPorSituacao(situacao);
            return comandas?.ToListDto();
        }

        public async Task<ComandaDto> ConfirmarAsync(Guid comandaId)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!_validador.PermiteAlterarSituacao(comandaId))
            {
                return null;
            }

            Comanda comanda = await _repository.Confirmar(comandaId);
            ComandaDto dto = comanda.ToDto();
            await SalvarCache(dto.Id, dto);
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

            Comanda comanda = await _repository.Fechar(comandaId);
            ComandaDto dto = comanda.ToDto();
            await SalvarCache(dto.Id, dto);
            await _mensageriaService.EnviarAsync(new ComandaFecharCommand(dto));
            return comanda?.ToDto();
        }
    }
}