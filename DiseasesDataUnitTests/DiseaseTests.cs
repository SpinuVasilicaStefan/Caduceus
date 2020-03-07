using DiseasesData;
using System;
using Xunit;

namespace DiseasesDataUnitTests
{
    public class DiseaseTests
    {
        [Fact]
        public void WhenCreatedThenIdIsValid()
        {
            Disease disease = new Disease();

            Assert.NotEqual(Guid.Empty, disease.Id);
        }

        [Fact]
        public void WhenCreatedThenNamesIsNull()
        {
            Disease disease = new Disease();

            Assert.Null(disease.Names);
        }

        [Fact]
        public void WhenCreatedThenStringNamesIsNull()
        {
            Disease disease = new Disease();

            Assert.Null(disease.StringNames);
        }

        [Fact]
        public void WhenTwoAreCreatedThenIdsAreDifferent()
        {
            Disease disease1 = new Disease(), disease2 = new Disease();

            Assert.NotEqual(disease1.Id, disease2.Id);
        }
    }
}
