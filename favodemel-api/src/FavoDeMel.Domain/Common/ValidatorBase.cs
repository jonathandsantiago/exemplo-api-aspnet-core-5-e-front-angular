using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Framework.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Common
{
    public abstract class ValidatorBase<TId, TEntity, TRepository> : IValidator<TId, TEntity>
         where TEntity : Entity<TId>
         where TRepository : IRepositoryBase<TId, TEntity>
    {
        public virtual IList<string> Mensagens { get; protected set; }
        public virtual bool IsValido
        {
            get
            {
                return !Mensagens.Any();
            }
        }
        public virtual TRepository _repository { get; protected set; }

        public ValidatorBase()
        {
            Mensagens = new List<string>();
        }

        public ValidatorBase(TRepository repository)
            : this()
        {
            _repository = repository;
        }

        public virtual async Task<bool> Validar(TEntity entity)
        {
            ValidarAnnotations(entity);
            return await Task.Run(() => IsValido);
        }

        public virtual async Task<bool> PermiteExcluir(TId id)
        {
            return await Task.Run(() => IsValido);
        }

        public virtual void AddMensagem(string mensagem)
        {
            Mensagens.Add(mensagem);
        }

        public virtual void ValidarAnnotations(TEntity entity)
        {
            PropertyInfo[] propertys = typeof(TEntity).GetProperties();

            if (!propertys.Any())
            {
                return;
            }

            foreach (var property in propertys)
            {
                var attribute = entity.GetAttribute<MaxLengthAttribute>(property.Name);

                if (attribute == null)
                {
                    continue;
                }

                var maxLength = attribute.Length;

                if (property.GetValue(entity) is string valueString && valueString.Length > maxLength)
                {
                    Mensagens.Add($"{entity.GetDisplayName(property.Name)} não pode ultrapassar máximo de {maxLength} caracteres.");
                }
            }
        }
    }
}