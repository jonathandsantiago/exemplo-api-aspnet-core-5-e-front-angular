using FavoDeMel.Domain.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Common
{
    public abstract class ValidatorBase<TEntity> : IValidator<TEntity>
         where TEntity : class
    {
        public virtual IList<string> Mensagens { get; protected set; }
        public virtual bool IsValido => !Mensagens.Any();

        protected ValidatorBase()
        {
            Mensagens = new List<string>();
        }

        public virtual async Task<bool> Validar(TEntity entity)
        {
            return await Task.Run(() => IsValido);
        }

        public virtual void AddMensagem(string mensagem)
        {
            Mensagens.Add(mensagem);
        }

        public virtual void AddMensagem(string columnName, string message)
        {
            Mensagens.Add(JsonConvert.SerializeObject(new { columnName, message }));
        }
    }
}
