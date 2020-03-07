using DiseasesData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DiseasesDataUnitTests
{
    public class CauseTests
    {
        [Fact]
        public void WhenCreatedThenIdIsValid()
        {
            Cause cause = new Cause(new Disease(), new Disease());

            Assert.NotEqual(Guid.Empty, cause.Id);
        }

        [Fact]
        public void WhenTwoAreCreatedThenIdsAreDifferent()
        {
            Cause cause1 = new Cause(new Disease(), new Disease()),
                  cause2 = new Cause(new Disease(), new Disease());

            Assert.NotEqual(cause1.Id, cause2.Id);
        }
    }
}
