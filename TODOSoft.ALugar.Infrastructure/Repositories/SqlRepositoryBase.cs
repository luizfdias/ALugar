using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SmartCA.Infrastructure.RepositoryFramework;
using TODOSoft.ALugar.Infrastructure.DomainBase;

namespace SmartCA.Infrastructure.Repositories
{
    public abstract class SqlRepositoryBase<T>: RepositoryBase<T> where T : EntityBase
    {
        /// <summary> 
        /// The delegate signature required for callback methods
        /// </summary> 
        /// <param name="entityAggregate"> </param> 
        /// <param name="childEntityKeyValue"> </param> 
        public delegate void AppendChildData(T entityAggregate, object childEntityKeyValue);

        private Database _database;
        private IEntityFactory<T> _entityFactory;
        private Dictionary<string, AppendChildData> _childCallbacks;

        protected SqlRepositoryBase(): this(null)
        {
        }

        protected SqlRepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._database = DatabaseFactory.CreateDatabase();
            this._entityFactory = EntityFactoryBuilder.BuildFactory<T>();

            // _childCallBacks é um dicionário que recebe o nome do campo na tabela sql e um método de callback associado 
            // que pode ser usado para preencher outras entidades do agregado.
            this._childCallbacks = new Dictionary<string, AppendChildData>();

            // Caso seja necessário chamar algum callback depois da entidade ser construída. Opcional.
            this.BuildChildCallbacks();
        }

        // Passa todas as responsabilidades da UnitOfWork para a classe derivada.
        protected abstract void BuildChildCallbacks();
        public abstract override T FindBy(object key);
        protected abstract override void PersistNewItem(T item);
        protected abstract override void PersistUpdatedItem(T item);
        protected abstract override void PersistDeletedItem(T item);

        protected Database Database
        {
            get { return this._database; }
        }

        protected Dictionary<string, AppendChildData> ChildCallbacks
        {
            get { return this._childCallbacks; }
        }

        /// <summary>
        /// Simplesmente executa a consulta SQL e retorna um IDataReader.
        /// </summary>
        /// <param name="sql">A string do comando SQL.</param>
        /// <returns>O IDataReader.</returns>
        protected IDataReader ExecuteReader(string sql)
        {
            DbCommand command = this._database.GetSqlStringCommand(sql);
            return this._database.ExecuteReader(command);
        }

        /// <summary>
        /// Para cada registro encontrado na base executada através do comando sql, preenche a entidade respectiva.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual T BuildEntityFromSql(string sql)
        {
            T entity = default(T);
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    entity = this.BuildEntityFromReader(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual T BuildEntityFromReader(IDataReader reader)
        {
            T entity = this._entityFactory.BuildEntity(reader);

            // Caso exista algum callback, será percorrida a lista a procura das foreign keys para o preenchimento dos agregados.
            if (this._childCallbacks != null && this._childCallbacks.Count > 0)
            {
                object childKeyValue = null;
                DataTable columnData = reader.GetSchemaTable();
                foreach (string childKeyName in this._childCallbacks.Keys)
                {
                    childKeyValue = DataHelper.ReaderContainsColumnName(columnData, childKeyName) ? reader[childKeyName] : null;

                    this._childCallbacks[childKeyName](entity, childKeyValue);
                }
            }

            return entity;
        }

        protected virtual List<T> BuildEntitiesFromSql(string sql)
        {
            var entities = new List<T>();
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    entities.Add(this.BuildEntityFromReader(reader));                    
                }
            }

            return entities;
        }
    }
}
