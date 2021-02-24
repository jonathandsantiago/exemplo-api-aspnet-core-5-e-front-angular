using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Domain.Models
{
    public class FiltroComanda : Filtro
    {
        public string Comandas { get; set; }

        public IList<int> ComandasIds
        {
            get
            {
                if (Comandas == null)
                {
                    return new List<int>();
                }

                return Comandas.Split(',').Select(c => int.Parse(c)).ToList();
            }
        }
    }
}