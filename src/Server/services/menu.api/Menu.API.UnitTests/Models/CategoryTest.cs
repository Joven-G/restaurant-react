﻿using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Menu.API.Models;
using Xunit;

namespace Menu.API.UnitTests.Models
{
    public class CategoryTest
    {
        [Theory, AutoDomainData]
        public void Test_category_auto_properties(WritablePropertyAssertion assertion)
        {
            assertion.Verify(new Properties<Category>().Select(c => c.Name));
            assertion.Verify(new Properties<Category>().Select(c => c.ShortName));
            assertion.Verify(new Properties<Category>().Select(c => c.Color));
            assertion.Verify(new Properties<Category>().Select(c => c.Foods));
        }
    }
}
