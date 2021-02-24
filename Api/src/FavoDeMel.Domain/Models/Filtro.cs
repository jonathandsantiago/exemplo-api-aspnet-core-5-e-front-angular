﻿using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Domain.Models
{
    public class Filtro
    {
        /// <summary>
        /// 
        /// </summary>
        public Filtro()
        {
            Limite = 10;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Pagina { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Limite { get; set; }
    }

}
