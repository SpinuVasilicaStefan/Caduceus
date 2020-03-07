using DiseasesData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DiseasesDataUnitTests
{
    public class NameTests
    {
        [Fact]
        public void WhenCreatedThenIdIsValid()
        {
            Name name = new Name(new Disease(), "test name");

            Assert.NotEqual(Guid.Empty, name.Id);
        }

        [Fact]
        public void WhenCreatedThenDiseaseIdIsValid()
        {
            Disease disease = new Disease();
            Name name = new Name(disease.Id, "test name");

            Assert.Equal(disease.Id, name.DiseaseId);
        }

        [Fact]
        public void WhenCreatedThenDiseaseIsNull()
        {
            Name name = new Name(new Disease(), "test name");

            Assert.Null(name.Disease);
        }

        [Fact]
        public void WhenCreatedThenValueIsValid()
        {
            Name name = new Name(new Disease(), "test name");

            Assert.Equal("test name", name.Value);
        }

        [Fact]
        public void WhenTwoAreCreatedThenIdsAreDifferent()
        {
            Name name1 = new Name(new Disease(), "test name1"), name2 = new Name(new Disease(), "test name2");

            Assert.NotEqual(name1.Id, name2.Id);
        }
    }
}
