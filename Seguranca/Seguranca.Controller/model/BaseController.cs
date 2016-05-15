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
            this.clearQuery();
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

        public virtual ModelClass encontrar(Func<ModelClass, bool> w)
        {
            return this.listarGenerico().FirstOrDefault(w);
        }

        protected virtual IQueryable<ModelClass> listarGenerico()
        {
            return this.DbSet;
        }

        public virtual int Adicionar(ModelClass model)
        {
            this.antesAdicionar(model);
            this.antesSalvar(AcaoController.Adicionar, model);
            this.DbSet.Add(model);
            return this.context.SaveChanges();
        }

        public virtual int alterar(ModelClass model)
        {
            this.antesAlterar(model);
            this.antesSalvar(AcaoController.Alterar, model);
            this.context.Entry<ModelClass>(model).State = EntityState.Modified;
            return this.context.SaveChanges();
        }

        public virtual int Remover(ModelClass model)
        {
            this.DbSet.Remove(model);
            return this.context.SaveChanges();
        }

        public virtual void antesAdicionar(ModelClass model)
        { }

        public virtual void antesAlterar(ModelClass model)
        { }

        public virtual void antesSalvar(AcaoController acao, ModelClass model)
        { }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Escopo padrão que é aplicado ao controller sempre que inicializado ou que uma query é finalizada.
        /// </summary>
        /// <returns>Objeto padrão IQueryable.</returns>
        public virtual IQueryable<ModelClass> defaultScope()
        {
            return this.DbSet;
        }

        /// <summary>
        /// Guarda a cache das queries que estão sendo montadas.
        /// </summary>
        public IQueryable<ModelClass> query { get; protected set; }

        /// <summary>
        /// Faz a inclusão de um método para que ele EF possa fazer um JOIN.
        /// </summary>
        /// <param name="path">Caminho dividido por "." para que o sistema faça o JOIN.</param>
        /// <returns>A própria referencia do controller.</returns>
        public BaseController<ModelClass> include(String path)
        {
            this.query = this.query.Include(path);
            return this;
        }

        public BaseController<ModelClass> include<TProperty>(Expression<Func<ModelClass, TProperty>> property)
        {
            this.query = this.query.Include(property);
            return this;
        }

        protected BaseController<ModelClass> where(Expression<Func<ModelClass, bool>> where)
        {
            this.query = this.query.Where(where);
            return this;
        }

        public BaseController<ModelClass> orderBy<TKey>(Expression<Func<ModelClass, TKey>> orderBy)
        {
            this.query = this.query.OrderBy(orderBy);
            return this;
        }

        protected BaseController<ModelClass> thenBy<TKey>(Expression<Func<ModelClass, TKey>> thenBy)
        {
            this.query = ((IOrderedQueryable<ModelClass>)this.query).ThenBy(thenBy);
            return this;
        }

        protected BaseController<ModelClass> orderByDescending<TKey>(Expression<Func<ModelClass, TKey>> orderBy)
        {
            this.query = this.query.OrderByDescending(orderBy);
            return this;
        }

        protected BaseController<ModelClass> thenByDescending<TKey>(Expression<Func<ModelClass, TKey>> thenBy)
        {
            this.query = ((IOrderedQueryable<ModelClass>)this.query).ThenByDescending(thenBy);
            return this;
        }

        /// <summary>
        /// Limpa a query que está sendo montada.
        /// </summary>
        public void clearQuery()
        {
            this.query = this.defaultScope();
        }

        /// <summary>
        /// Retorna um primeiro registro da query.
        /// </summary>
        /// <param name="where">Condição para a busca do </param>
        /// <returns></returns>
        public ModelClass find(Func<ModelClass, bool> where = null)
        {
            ModelClass result;
            if (where != null)
                result = this.query.FirstOrDefault(where);
            else
                result = this.query.FirstOrDefault();
            this.clearQuery();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pks"></param>
        /// <returns></returns>
        public ModelClass findBy(IDictionary<string, object> pks)
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
                typeof(Queryable), "Where", new Type[] { this.query.ElementType }, this.query.Expression,
                Expression.Lambda<Func<ModelClass, bool>>(eBody, new ParameterExpression[] { peModel }));
            this.query = this.query.Provider.CreateQuery<ModelClass>(whereCallExpression);
            return this.find();
        }

        /// <summary>
        /// Faz a busca pela chave primária.
        /// </summary>
        /// <param name="keyValues">Valores das chaves primárias.</param>
        /// <returns>A instância do modelo encontrado. Caso não encontrado retornará nulo.</returns>
        public ModelClass FindByPk(params object[] keyValues)
        {
            List<String> keyFields = CustomModel.getPrimaryKey(typeof(ModelClass));
            if (keyFields.Count != keyValues.Count())
                throw new ArgumentException("Parameters and primary key count don't match.");

            Dictionary<string, object> keys = new Dictionary<string, object>();
            for (int i = 0; i < keyFields.Count; i++)
                keys[keyFields[i]] = keyValues[i];

            return this.findBy(keys);
        }

        /// <summary>
        /// Retorna uma lista de objetos. Com a possibilidade de adicionar mais um where.
        /// </summary>
        /// <param name="where">Condição adicionada na query antes da execução.</param>
        /// <param name="offset">Quantidade de deve ser pulada.</param>
        /// <param name="count">Quantidade de registros que devem ser retornados.</param>
        /// <returns>Uma lista de registros que combinam com as condições </returns>
        public List<ModelClass> findAll(Expression<Func<ModelClass, bool>> where, int? offset = null, int? count = null)
        {
            if (where != null)
                this.where(where);
            return this.findAll(offset, count);
        }

        /// <summary>
        /// Retorna uma lista de objetos.
        /// </summary>
        /// <param name="offset">Quantidade de deve ser pulada.</param>
        /// <param name="count">Quantidade de registros que devem ser retornados.</param>
        /// <returns></returns>
        public List<ModelClass> findAll(int? offset = null, int? count = null)
        {
            List<ModelClass> result;

            if (offset.HasValue && count.HasValue)
                result = this.query.Skip(offset.Value).Take(count.Value).ToList();
            else if (count.HasValue)
                result = this.query.Take(count.Value).ToList();
            else if (offset.HasValue)
                result = this.query.Skip(offset.Value).ToList();
            else
                result = this.query.ToList();

            this.clearQuery();
            return result;
        }

        public bool exists()
        {
            Boolean result = this.query.Any();
            this.clearQuery();
            return result;
        }

        /// <summary>
        /// Conta quantos registros batem com a query e não limpa a expressão da consulta.
        /// </summary>
        /// <returns></returns>
        public int countAndDontClear()
        {
            return this.query.Count();
        }

        /// <summary>
        /// Conta quantos registros batem com a query. Depois limpa a mesma.
        /// </summary>
        /// <returns></returns>
        public int count()
        {
            int result = this.query.Count();
            clearQuery();
            return result;
        }

        /// <summary>
        /// Cria uma instância do modelo controlado por este controller já anexada ao seu DbSet.
        /// </summary>
        /// <returns>Instância do modelo deste controller.</returns>
        public ModelClass create()
        {
            return this.DbSet.Create();
        }
    }
}
