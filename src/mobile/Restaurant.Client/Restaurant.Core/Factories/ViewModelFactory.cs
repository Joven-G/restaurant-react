﻿using Autofac;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Restaurant.Core.Factories
{
    [ExcludeFromCodeCoverage]
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly ILifetimeScope _container;
        private readonly IDiagnosticsFacade _diagnosticsFacade;

        public ViewModelFactory(
            ILifetimeScope container,
            IDiagnosticsFacade diagnosticsFacade)
        {
            _container = container;
            _diagnosticsFacade = diagnosticsFacade;
        }

        public INavigatableViewModel GetViewModel(Type viewModelType)
        {
            try
            {
                return _container.Resolve(viewModelType) as INavigatableViewModel;
            }
            catch (Exception e)
            {
                _diagnosticsFacade.TrackError(e);
#if DEBUG
                throw;
#endif
            }

#pragma warning disable CS0162 // Unreachable code detected
            return null;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public INavigatableViewModel GetMainViewModel(Type viewModelType, string platform)
        {
            try
            {
                return _container.ResolveNamed(platform, viewModelType) as INavigatableViewModel;
            }
            catch (Exception ex)
            {
                _diagnosticsFacade.TrackError(ex);
#if DEBUG
                throw;
#endif
            }

#pragma warning disable CS0162 // Unreachable code detected
            return null;
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}