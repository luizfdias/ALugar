using System.Configuration;

namespace TODOSoft.ALugar.Infrastructure.RepositoryFramework.Configuration
{
    public class RepositorySettings : ConfigurationSection
    {
        [ConfigurationProperty(RepositoryMappingConstants.ConfigurationPropertyName, IsDefaultCollection = true)]
        public RepositoryMappingCollection RepositoryMappings
        {
            get { return (RepositoryMappingCollection) base[RepositoryMappingConstants.ConfigurationPropertyName]; }
        }
    }
}
