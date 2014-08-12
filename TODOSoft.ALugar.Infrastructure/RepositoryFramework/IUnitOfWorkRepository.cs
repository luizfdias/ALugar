using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOSoft.ALugar.Infrastructure.DomainBase;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(EntityBase item); 
        void PersistUpdatedItem(EntityBase item); 
        void PersistDeletedItem(EntityBase item);
    }
}
