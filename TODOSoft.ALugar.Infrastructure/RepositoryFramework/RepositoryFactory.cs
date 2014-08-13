﻿using System;
using System.Collections.Generic;
using System.Configuration;
using TODOSoft.ALugar.Infrastructure.DomainBase;
using TODOSoft.ALugar.Infrastructure.RepositoryFramework;
using TODOSoft.ALugar.Infrastructure.RepositoryFramework.Configuration;

namespace SmartCA.Infrastructure.RepositoryFramework
{
    public static class RepositoryFactory
    {
        private static readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();

        /// <summary> 
        /// Gets or creates an instance of the requested interface. Once a
        /// repository is created and initialized, it is cached, and all
        /// future requests for the repository will come from the cache.
        /// </summary> 
        /// <typeparam name="TRepository"> The interface of the repository
        /// to create. </typeparam>
        /// < typeparam name="TEntity"> The type of the EntityBase that the
        /// repository is for. </typeparam>
        /// <returns> An instance of the interface requested. </returns> 
        public static TRepository GetRepository<TRepository, TEntity>() where TRepository : class, IRepository<TEntity> where TEntity : EntityBase
        {
            // Initialize the provider’s default value
            TRepository repository;

            string interfaceShortName = typeof(TRepository).Name;

            // See if the provider was already created and is in the cache
            if (!_repositories.ContainsKey(interfaceShortName))
            {
                // Not there, so create it

                // Get the repositoryMappingsConfiguration config section
                var settings = (RepositorySettings)ConfigurationManager.GetSection(RepositoryMappingConstants.RepositoryMappingsConfigurationSectionName);

                // Create the repository, and cast it to the interface specified
                repository = Activator.CreateInstance(Type.GetType(settings.RepositoryMappings[interfaceShortName].RepositoryFullTypeName)) as TRepository;

                // Add the new provider instance to the cache
                _repositories.Add(interfaceShortName, repository);
            }
            else
            {
                // The provider was in the cache, so retrieve it
                repository = (TRepository)_repositories[interfaceShortName];
            }

            return repository;
        }
    }
}
