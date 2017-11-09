﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.ViewModels;

namespace Restaurant.UnitTests.ViewModels
{
	[TestFixture]
	public class FoodsViewModelTests : BaseAutoMockedTest<FoodsViewModel>
	{
		[Test]
		public void Title_should_be_Foods()
		{
			Assert.That(ClassUnderTest.Title, Is.EqualTo("Foods"));
		}

		[Test, AutoDomainData]
		public async Task LoadFoods_should_load_foods_and_set_to_Foods(IEnumerable<FoodDto> foods)
		{
			// given
			var viewModel = ClassUnderTest;
			GetMock<IFoodsApi>().Setup(x => x.GetFoods()).Returns(Task.FromResult(foods));

			// when
			await viewModel.LoadFoods();

			// then
			Assert.That(viewModel.Foods, Is.Not.Null);
			Assert.That(viewModel.Foods.Count, Is.EqualTo(foods.Count()));
		}

		[Test, AutoDomainData]
		public async Task When_food_selects_should_navigate_to_FoodDetailPage(IEnumerable<FoodDto> foods)
		{
			// given
			var viewModel = ClassUnderTest;
			var selectedFood = foods.FirstOrDefault();
			var basketViewModel = GetMock<IBasketViewModel>();
			var navigationService = GetMock<INavigationService>();
			var foodDetailViewModel = new FoodDetailViewModel(selectedFood, basketViewModel.Object, navigationService.Object);

			GetMock<IFoodsApi>().Setup(x => x.GetFoods()).Returns(Task.FromResult(foods));
			GetMock<IFoodDetailViewModelAdapter>().Setup(x => x.GetFoodDetailViewModel(selectedFood))
				.Returns(foodDetailViewModel);
			
			// when
			await viewModel.LoadFoods();
			viewModel.SelectedFood = selectedFood;

			// then
			GetMock<INavigationService>().Verify(x => x.NavigateAsync(foodDetailViewModel), Times.Once);
		}
	}
}
