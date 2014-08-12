/// <summary>
/// Essa classe é baseada no padrão "Layered Supertype", relatado pelo Martin Fowler.
/// Basicamente ele diz que se algo é comum aos tipos do modelo, pode ser incluído em um super tipo.
/// Este supertipo fica na camada de infraestrutura, pois não é algo efetivamente do domínio.
/// </summary>

namespace TODOSoft.ALugar.Infrastructure.DomainBase
{
    public abstract class EntityBase
    {
        private object _key;

        /// <summary>
        /// O construtor padrao.
        /// </summary>
        protected EntityBase() : this(null) { }

        /// <summary>
        /// Overload do construtor.
        /// </summary>
        /// <param name="key"> Um <see cref="System.Object" /> que
        /// representa o identificador primário da classe.</param>
        protected EntityBase(object key)
        {
            this._key = key;
        }

        /// <summary>
        ///  Um <see cref="System.Object" /> que representa
        /// o identificador primário da classe.
        /// </summary>
        public object Key
        {
            get { return this._key; }
        }

        #region Testes de igualdade

        /// <summary>
        /// Determina se a entidade especificada é igual a 
        /// instância corrente.
        /// </summary>
        /// <param name="entity"> Um <see cref="System.Object"/> que será
        /// comparado com a instância corrente.</param>
        /// <returns>Retorna True se a entidade passada é igual a 
        /// instância corrente.</returns>
        public override bool Equals(object entity)
        {
            if (!(entity is EntityBase))
            {
                return false;
            }

            return (this == entity as EntityBase);
        }

        /// <summary>
        /// Overload da operação para determinar a igualdade.
        /// </summary>
        /// <param name="base1">A primeira instância de <see cref="EntityBase"/>.</param>
        /// <param name="base2">A segunda instância de <see cref="EntityBase"/>.</param>
        /// <returns></returns>
        public static bool operator == (EntityBase base1, EntityBase base2)
        {
            // Se os dois objetos são nulos, são iguais.
            if (base1 as object == null && base2 as object == null)
            {
                return true;
            }

            if (base1 as object == null || base2 as object == null)
            {
                return false;
            }

            return base1.Key == base2.Key;
        }

        /// <summary>
        /// Overload da operação para determinar a desigualdade.
        /// </summary>
        /// <param name="base1">A primeira instância de <see cref="EntityBase"/>.</param>
        /// <param name="base2">A segunda instância de <see cref="EntityBase"/>.</param>
        /// <returns></returns>
        public static bool operator !=(EntityBase base1, EntityBase base2)
        {
            return (!(base1 == base2));
        }

        /// <summary>
        /// Serve como uma função hash do tipo.
        /// </summary>
        /// <returns>Retorna o hash code da chave corrente.</returns>
        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        #endregion
    }
}
