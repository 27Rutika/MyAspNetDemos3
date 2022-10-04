using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace MyDemos.xUnitTests
{
    public partial class CategoriesApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public CategoriesApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }   
    }
}
