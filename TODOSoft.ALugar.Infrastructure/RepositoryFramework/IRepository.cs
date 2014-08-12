/// <summary>
/// Essa classe é genérica para todos os outros repositórios.
/// Um adendo é que haverão outras interfaces dos repositórios na camada
/// de domínio. Os repositórios concretos estarão na camada de infra.
/// Isto é um padrão de separação de interfaces, onde é possível ter
/// um desaclopamento maior, pois o domínio irá conhecer somente as 
/// interfaces do repositório...
/// </summary>

using TODOSoft.ALugar.Infrastructure.DomainBase;

namespace TODOSoft.ALugar.Infrastructure.RepositoryFramework
{
    public interface IRepository<T> where T : EntityBase
    {
        T FindBy(object key);
        void Add(T item);
        T this[object key] { get; set; }
        void Remove(T item);
    }
}
