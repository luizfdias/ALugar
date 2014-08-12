/// <summary>
/// Essa classe mapeia as configurações, mais especificamente os atributos da configuração
/// que nesse caso são: InterfaceShortTypeName e RepositoryFullTypeName.
/// </summary>

using System.Configuration;

namespace TODOSoft.ALugar.Infrastructure.RepositoryFramework.Configuration
{
    public sealed class RepositoryMappingElement : ConfigurationElement
    {
        [ConfigurationProperty(RepositoryMappingConstants.InterfaceShortTypeNameAttributeName, IsKey = true, IsRequired = true)]
        public string InterfaceShortTypeName
        {
            get
            {
                return (string)this[RepositoryMappingConstants.InterfaceShortTypeNameAttributeName];
            }
            set
            {
                this[RepositoryMappingConstants.InterfaceShortTypeNameAttributeName] = value;
            }
        }

        [ConfigurationProperty(RepositoryMappingConstants.RepositoryFullTypeNameAttributeName, IsRequired = true)]
        public string RepositoryFullTypeName
        {
            get
            {
                return (string)this[RepositoryMappingConstants.RepositoryFullTypeNameAttributeName];
            }
            set
            {
                this[RepositoryMappingConstants.RepositoryFullTypeNameAttributeName] = value;
            }
        }
    }
}
