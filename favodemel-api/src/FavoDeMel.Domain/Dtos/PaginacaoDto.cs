using System;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Dtos
{
    public class PaginacaoDto<TDto>
    {
        /// <summary>
        /// Determina os itens do tipo especifico da coleção
        /// </summary>
        public IList<TDto> Itens { get; set; }

        /// <summary>
        /// Determina o total dos itens na coleção
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Determina a quantidade maxima de registros a ser carregado na coleção por paginas
        /// </summary>
        public int Limite { get; set; }

        /// <summary>
        /// Determina a pagina atual
        /// </summary>
        public int Pagina { get; set; }

        /// <summary>
        /// Determina o total de paginas que contem a coleção
        /// </summary>
        public int Paginas
        {
            get
            {
                if (Total == 0 || Limite == 0)
                {
                    return 0;
                }

                int pages = Math.Abs(Total / Limite);

                if ((Limite * pages) < Total)
                {
                    pages++;
                }

                return pages;
            }
        }

        public PaginacaoDto()
        {
            Itens = new List<TDto>();
        }

        public PaginacaoDto(int total, int limite, int pagina)
            : this()
        {

            Total = total;
            Limite = limite;
            Pagina = pagina;
        }
    }
}