using TODOSoft.ALugar.Infrastructure.DomainBase;
using TODOSoft.ALugar.Infrastructure.RepositoryFramework;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public abstract class RepositoryBase<T> : IRepository<T>, IUnitOfWorkRepository where T : EntityBase
    {
        private IUnitOfWork _unitOfWork;

        protected RepositoryBase() : this(null)
        {
            
        }

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region IRepository <T> Members

        public abstract T FindBy(object key);

        public void Add(T item)
        {
            if (this._unitOfWork != null)
            {
                this._unitOfWork.RegisterAdded(item, this);
            }
        }

        public T this[object key]
        {
            get
            {
                return this.FindBy(key);
            }
            set
            {
                if (this.FindBy(key) == null)
                {
                    this.Add(value);
                }
                else
                {
                    this._unitOfWork.RegisterChanged(value, this);
                }
            }
        }

        public void Remove(T item)
        {
            if (this._unitOfWork != null)
            {
                this._unitOfWork.RegisterRemoved(item, this);
            }
        }

        #endregion

        #region IUnitOfWorkRepository Members
        public void PersistNewItem(EntityBase item)
        {
            this.PersistNewItem((T)item);
        }

        public void PersistUpdatedItem(EntityBase item)
        {
            this.PersistUpdatedItem((T)item);
        }

        public void PersistDeletedItem(EntityBase item)
        {
            this.PersistDeletedItem((T)item);
        }
        #endregion 

        protected abstract void PersistNewItem(T item);
        protected abstract void PersistUpdatedItem(T item);
        protected abstract void PersistDeletedItem(T item);
    }
}
