using FavoDeMel.Domain.Common;

namespace FavoDeMel.Domain.Comandas
{
    public class ComandaValidator : ValidatorBase<int, Comanda, IComandaRepository>
    {
        public ComandaValidator(IComandaRepository repository) :
            base(repository)
        { }
    }
}
