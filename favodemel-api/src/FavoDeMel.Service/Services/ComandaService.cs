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

        public async Task<ComandaDto> ObterPorId(Guid id)
        {
            var comandoCache = await ObterPorIdInCache<ComandaDto, Guid>(id);

            if (comandoCache != null)
            {
                return comandoCache;
            }

            var comanda = await _repository.ObterPorId(id);
            ComandaDto dto = comanda?.ToDto();
            if (dto != null)
            {
                await SalvarCache(dto.Id, dto);
            }
            return dto;
        }

        public async Task<ComandaDto> Inserir(ComandaDto comandaDto)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!await _validador.Validar(comandaDto))
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
            Comanda comandaDb = await _repository.Inserir(comanda);
            ComandaDto dto = comandaDb.ToDto();
            await SalvarCache(dto.Id, dto);
            await _mensageriaService.Enviar(new ComandaCadastroCommand(dto));
            return dto;
        }

        public async Task<ComandaDto> Editar(ComandaDto comandaDto)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!await _validador.Validar(comandaDto))
            {
                return null;
            }

            Comanda comanda = comandaDto.ToEntity();
            comanda.Prepare();
            await _repository.Editar(comanda);
            ComandaDto dto = comanda.ToDto();
            await SalvarCache(dto.Id, dto);
            await _mensageriaService.Enviar(new ComandaEditarCommand(dto));
            return dto;
        }

        public async Task<IEnumerable<ComandaDto>> ObterTodosPorSituacao(ComandaSituacao situacao)
        {
            var comandas = await _repository.ObterTodosPorSituacao(situacao);
            return comandas?.ToListDto();
        }

        public async Task<ComandaDto> Confirmar(Guid comandaId)
        {
            using var dbTransaction = _repository.BeginTransaction(_validador);
            if (!_validador.PermiteAlterarSituacao(comandaId))
            {
                return null;
            }

            Comanda comanda = await _repository.Confirmar(comandaId);
            ComandaDto dto = comanda.ToDto();
            await SalvarCache(dto.Id, dto);
            await _mensageriaService.Enviar(new ComandaConfirmarCommand(dto));
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
            await _mensageriaService.Enviar(new ComandaFecharCommand(dto));
            return comanda?.ToDto();
        }
    }
}