using DiseasesData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DiseasesDataUnitTests
{
    public class DiseasesContextTests
    {
        private void clearContext(DiseasesContext context)
        {
            context.Diseases.RemoveRange(context.Diseases);
            context.Names.RemoveRange(context.Names);
            context.Causes.RemoveRange(context.Causes);
            context.SaveChanges();
        }

        [Fact]
        public void WhenNewCauseIsInsertedThenItMustBeUnique()
        {
            using (DiseasesContext context = new DiseasesContext())
            {
                clearContext(context);

                Disease disease1 = new Disease(), disease2 = new Disease();
                Cause cause1 = new Cause(disease1, disease2), cause2 = new Cause(disease1, disease2);

                context.Diseases.Add(disease1);
                context.Diseases.Add(disease2);
                context.Causes.Add(cause1);
                context.SaveChanges();
                try
                {
                    context.Causes.Add(cause2);
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    Assert.Equal(1, context.Causes.Count());
                }
            }
        }

        [Fact]
        public void WhenNewDiseaseInsertedThenUpdateSuccessful()
        {
            using (DiseasesContext context = new DiseasesContext())
            {
                clearContext(context);

                Disease disease = new Disease();

                context.Diseases.Add(disease);
                context.SaveChanges();

                Assert.Equal(1, context.Diseases.Count());
            }
        }

        [Fact]
        public void WhenNewNameInsertedThenUpdateSuccessful()
        {
            using (DiseasesContext context = new DiseasesContext())
            {
                clearContext(context);

                Disease disease = new Disease();
                Name name = new Name(disease, "test name");

                context.Diseases.Add(disease);
                context.Names.Add(name);
                context.SaveChanges();

                Assert.Equal(1, context.Names.Count());
            }
        }

        [Fact]
        public void WhenNewCauseInsertedThenUpdateSuccessful()
        {
            using (DiseasesContext context = new DiseasesContext())
            {
                clearContext(context);

                Disease disease1 = new Disease(), disease2 = new Disease();
                Cause cause = new Cause(disease1, disease2);

                context.Diseases.AddRange(disease1, disease2);
                context.Causes.Add(cause);
                context.SaveChanges();

                Assert.Equal(1, context.Causes.Count());
            }
        }
    }
}
