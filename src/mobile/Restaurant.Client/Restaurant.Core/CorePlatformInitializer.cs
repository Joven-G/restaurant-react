﻿using System.Diagnostics.CodeAnalysis;
using Autofac;
using AutoMapper;
using Refit;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Constants;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Factories;
using Restaurant.Abstractions.Providers;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Core.Adapters;
using Restaurant.Core.Facades;
using Restaurant.Core.Factories;
using Restaurant.Core.Mappers;
using Restaurant.Core.MockData;
using Restaurant.Core.Providers;
using Restaurant.Core.Services;
using Restaurant.Core.ViewModels;
using Restaurant.Core.ViewModels.Android;
using Restaurant.Core.ViewModels.Food;

namespace Restaurant.Core
{
    [ExcludeFromCodeCoverage]
    public abstract class CorePlatformInitializer : IPlatformInitializer
    {
        public static bool MockData = true;

        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(AutoMapperConfiguration.Configure()).As<IMapper>();

            builder.RegisterType<FoodsViewModel>().AsSelf();
            builder.RegisterType<FoodDetailViewModel>().AsSelf();

            builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
            builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
            builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();
            builder.RegisterType<OrdersViewModel>().AsSelf();
            builder.RegisterType<BasketViewModel>().As<IBasketViewModel>();
            builder.RegisterType<MasterViewModel>().As<IMasterViewModel>().SingleInstance();

            builder.RegisterType<DateTimeFacade>().As<IDateTimeFacade>();
            builder.RegisterType<FoodDetailViewModelFactory>().As<IFoodDetailViewModelFactory>();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<BasketItemsService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NavigationItemAdapter>().As<INavigationItemAdapter>();
            builder.RegisterType<OrderDtoAdapter>().As<IOrderDtoAdapter>();
            builder.RegisterType<IdentityModelTokenProvider>().As<ITokenProvider>();


            if (MockData)
            {
                builder.RegisterType<MockOrdersApi>().As<IOrdersApi>();
                builder.RegisterType<MockFoodsApi>().As<IFoodsApi>();
                builder.RegisterType<MockAccountApi>().As<IAccountApi>();
                builder.RegisterType<MockAuthenticationProvider>().As<IAuthenticationProvider>();
            }
            else
            {
                var foodApi = RestService.For<IFoodsApi>(ApiConstants.ApiClientUrl);
                var ordersApi = RestService.For<IOrdersApi>(ApiConstants.ApiClientUrl);
                var accountApi = RestService.For<IAccountApi>(ApiConstants.ApiClientUrl);

                builder.RegisterInstance(accountApi).As<IAccountApi>().SingleInstance();
                builder.RegisterInstance(foodApi).As<IFoodsApi>().SingleInstance();
                builder.RegisterInstance(ordersApi).As<IOrdersApi>().SingleInstance();
                builder.RegisterType<AuthenticationProvider>().As<IAuthenticationProvider>().SingleInstance();
            }

            RegisterTypes(builder);
            return builder.Build();
        }

        protected abstract void RegisterTypes(ContainerBuilder builder);
    }
}
