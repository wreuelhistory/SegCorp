using Seguranca.Entidades.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seguranca.Controller.model
{
    public enum AcaoController
    {
        Adicionar,
        Alterar
    }

    public abstract class BaseController<ModelClass> : Object where ModelClass : CustomModel
    {
        public BaseController(SegurancaDBContext context)
        {
            this.context = context;
            this.ClearQuery();
        }

        /// <summary>
        /// DbSet utilizado para as operações sobre este modelo.
        /// </summary>
        public abstract DbSet<ModelClass> DbSet
        {
            get;
        }

        /// <summary>
        /// Contexto utilizado nas operações deste controller.
        /// </summary>
        public SegurancaDBContext context { get; set; }

        public virtual ModelClass Encontrar(Func<ModelClass, bool> w)
        {
            return this.ListarGenerico().FirstOrDefault(w);
        }

        protected virtual IQueryable<ModelClass> ListarGenerico()
        {
            return this.DbSet;
        }

        public virtual int Adicionar(ModelClass model)
        {
            this.AntesAdicionar(model);
            this.AntesSalvar(AcaoController.Adicionar, model);
            this.DbSet.Add(model);
            return this.context.SaveChanges();
        }

        public virtual int Alterar(ModelClass model)
        {
            this.AntesAlterar(model);
            this.AntesSalvar(AcaoController.Alterar, model);
            this.context.Entry<ModelClass>(model).State = EntityState.Modified;
            return this.context.SaveChanges();
        }

        public virtual int Remover(ModelClass model)
        {
            this.DbSet.Remove(model);
            return this.context.SaveChanges();
        }

        public virtual void AntesAdicionar(ModelClass model)
        { }

        public virtual void AntesAlterar(ModelClass model)
        { }

        public virtual void AntesSalvar(AcaoController acao, ModelClass model)
        { }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Escopo padrão que é aplicado ao controller sempre que inicializado ou que uma query é finalizada.
        /// </summary>
        /// <returns>Objeto padrão IQueryable.</returns>
        public virtual IQueryable<ModelClass> DefaultScope()
        {
            return this.DbSet;
        }

        /// <summary>
        /// Guarda a cache das queries que estão sendo montadas.
        /// </summary>
        public IQueryable<ModelClass> Query { get; protected set; }

        /// <summary>
        /// Faz a inclusão de um método para que ele EF possa fazer um JOIN.
        /// </summary>
        /// <param name="path">Caminho dividido por "." para que o sistema faça o JOIN.</param>
        /// <returns>A própria referencia do controller.</returns>
        public BaseController<ModelClass> Include(String path)
        {
            this.Query = this.Query.Include(path);
            return this;
        }

        public BaseController<ModelClass> Include<TProperty>(Expression<Func<ModelClass, TProperty>> property)
        {
            this.Query = this.Query.Include(property);
            return this;
        }

        protected BaseController<ModelClass> Where(Expression<Func<ModelClass, bool>> where)
        {
            this.Query = this.Query.Where(where);
            return this;
        }

        public BaseController<ModelClass> OrderBy<TKey>(Expression<Func<ModelClass, TKey>> orderBy)
        {
            this.Query = this.Query.OrderBy(orderBy);
            return this;
        }

        protected BaseController<ModelClass> ThenBy<TKey>(Expression<Func<ModelClass, TKey>> thenBy)
        {
            this.Query = ((IOrderedQueryable<ModelClass>)this.Query).ThenBy(thenBy);
            return this;
        }

        protected BaseController<ModelClass> OrderByDescending<TKey>(Expression<Func<ModelClass, TKey>> orderBy)
        {
            this.Query = this.Query.OrderByDescending(orderBy);
            return this;
        }

        protected BaseController<ModelClass> ThenByDescending<TKey>(Expression<Func<ModelClass, TKey>> thenBy)
        {
            this.Query = ((IOrderedQueryable<ModelClass>)this.Query).ThenByDescending(thenBy);
            return this;
        }

        /// <summary>
        /// Limpa a query que está sendo montada.
        /// </summary>
        public void ClearQuery()
        {
            this.Query = this.DefaultScope();
        }

        /// <summary>
        /// Retorna um primeiro registro da query.
        /// </summary>
        /// <param name="where">Condição para a busca do </param>
        /// <returns></returns>
        public ModelClass Find(Func<ModelClass, bool> where = null)
        {
            ModelClass result;
            if (where != null)
                result = this.Query.FirstOrDefault(where);
            else
                result = this.Query.FirstOrDefault();
            this.ClearQuery();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pks"></param>
        /// <returns></returns>
        public ModelClass FindBy(IDictionary<string, object> pks)
        {
            // Gera a query automaticamente para fazer todos as condições utilizando o "Equal".
            ParameterExpression peModel = Expression.Parameter(typeof(ModelClass), "model");
            Expression lExp, rExp, eBody = null;
            foreach (KeyValuePair<string, object> pk in pks)
            {
                // "Campo" == "Valor"
                lExp = Expression.PropertyOrField(peModel, pk.Key); // Lado esquerdo da condição
                rExp = Expression.Constant(pk.Value); // Lado direito da condição

                if (eBody == null)
                    eBody = Expression.Equal(lExp, rExp);
                else
                    eBody = Expression.And(eBody, Expression.Equal(lExp, rExp));
            }
            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable), "Where", new Type[] { this.Query.ElementType }, this.Query.Expression,
                Expression.Lambda<Func<ModelClass, bool>>(eBody, new ParameterExpression[] { peModel }));
            this.Query = this.Query.Provider.CreateQuery<ModelClass>(whereCallExpression);
            return this.Find();
        }

        /// <summary>
        /// Faz a busca pela chave primária.
        /// </summary>
        /// <param name="keyValues">Valores das chaves primárias.</param>
        /// <returns>A instância do modelo encontrado. Caso não encontrado retornará nulo.</returns>
        public ModelClass FindByPk(params object[] keyValues)
        {
            List<String> keyFields = CustomModel.GetPrimaryKey(typeof(ModelClass));
            if (keyFields.Count != keyValues.Count())
                throw new ArgumentException("Parameters and primary key count don't match.");

            Dictionary<string, object> keys = new Dictionary<string, object>();
            for (int i = 0; i < keyFields.Count; i++)
                keys[keyFields[i]] = keyValues[i];

            return this.FindBy(keys);
        }

        /// <summary>
        /// Retorna uma lista de objetos. Com a possibilidade de adicionar mais um where.
        /// </summary>
        /// <param name="where">Condição adicionada na query antes da execução.</param>
        /// <param name="offset">Quantidade de deve ser pulada.</param>
        /// <param name="count">Quantidade de registros que devem ser retornados.</param>
        /// <returns>Uma lista de registros que combinam com as condições </returns>
        public List<ModelClass> FindAll(Expression<Func<ModelClass, bool>> where, int? offset = null, int? count = null)
        {
            if (where != null)
                this.Where(where);
            return this.FindAll(offset, count);
        }

        /// <summary>
        /// Retorna uma lista de objetos.
        /// </summary>
        /// <param name="offset">Quantidade de deve ser pulada.</param>
        /// <param name="count">Quantidade de registros que devem ser retornados.</param>
        /// <returns></returns>
        public List<ModelClass> FindAll(int? offset = null, int? count = null)
        {
            List<ModelClass> result;

            if (offset.HasValue && count.HasValue)
                result = this.Query.Skip(offset.Value).Take(count.Value).ToList();
            else if (count.HasValue)
                result = this.Query.Take(count.Value).ToList();
            else if (offset.HasValue)
                result = this.Query.Skip(offset.Value).ToList();
            else
                result = this.Query.ToList();

            this.ClearQuery();
            return result;
        }

        public bool exists()
        {
            Boolean result = this.Query.Any();
            this.ClearQuery();
            return result;
        }

        /// <summary>
        /// Conta quantos registros batem com a query e não limpa a expressão da consulta.
        /// </summary>
        /// <returns></returns>
        public int CountAndDontClear()
        {
            return this.Query.Count();
        }

        /// <summary>
        /// Conta quantos registros batem com a query. Depois limpa a mesma.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int result = this.Query.Count();
            ClearQuery();
            return result;
        }

        /// <summary>
        /// Cria uma instância do modelo controlado por este controller já anexada ao seu DbSet.
        /// </summary>
        /// <returns>Instância do modelo deste controller.</returns>
        public ModelClass Create()
        {
            return this.DbSet.Create();
        }
    }
}
