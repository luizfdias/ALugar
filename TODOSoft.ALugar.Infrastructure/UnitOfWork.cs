using SmartCA.Infrastructure.RepositoryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TODOSoft.ALugar.Infrastructure.DomainBase;

namespace SmartCA.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<EntityBase, IUnitOfWorkRepository> _addedEntities;
        private Dictionary<EntityBase, IUnitOfWorkRepository> _changedEntities;
        private Dictionary<EntityBase, IUnitOfWorkRepository> _deletedEntities;

        public UnitOfWork()
        {
            this._addedEntities = new Dictionary<EntityBase, IUnitOfWorkRepository>(); 
            this._changedEntities = new Dictionary<EntityBase, IUnitOfWorkRepository>(); 
            this._deletedEntities = new Dictionary<EntityBase, IUnitOfWorkRepository>();
        }

        #region IUnitOfWork Members                  
        public void RegisterAdded(EntityBase entity, IUnitOfWorkRepository repository)        
        {            
            this._addedEntities.Add(entity, repository);        
        }

        public void RegisterChanged(EntityBase entity, IUnitOfWorkRepository repository) 
        {
            this._changedEntities.Add(entity, repository); 
        }

        public void RegisterRemoved(EntityBase entity, IUnitOfWorkRepository repository)
        {
            this._deletedEntities.Add(entity, repository);
        }


        public void Commit()
        {
            using (var scope = new TransactionScope())
            {
                foreach (var entity in this._deletedEntities.Keys)
                {
                    this._deletedEntities[entity].PersistDeletedItem(entity);
                }

                foreach (var entity in this._addedEntities.Keys)
                {
                    this._deletedEntities[entity].PersistNewItem(entity);
                }

                foreach (var entity in this._changedEntities.Keys)
                {
                    this._deletedEntities[entity].PersistUpdatedItem(entity);
                }                

                scope.Complete();
            }

            this._deletedEntities.Clear();
            this._changedEntities.Clear();
            this._addedEntities.Clear();
        }

        #endregion
    }
}
