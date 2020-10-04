using System;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using Xunit;
using TMDbLibTests.JsonHelpers;

namespace TMDbLibTests
{
    public class ClientChangesTests : TestBase
    {
        [Fact]
        public async Task TestChangesMoviesAsync()
        {
            SearchContainer<ChangesListItem> page1 = await TMDbClient.GetChangesMoviesAsync(1);
            SearchContainer<ChangesListItem> oldChanges = await TMDbClient.GetChangesMoviesAsync(endDate: DateTime.UtcNow.AddMonths(-1));

            Assert.NotEmpty(page1.Results);
            Assert.NotEmpty(oldChanges.Results);

            // At least one item must be newer in page1
            Assert.Contains(page1.Results, x => oldChanges.Results.All(s => s.Id != x.Id));
        }

        [Fact]
        public async Task TestChangesPeopleAsync()
        {
            SearchContainer<ChangesListItem> page1 = await TMDbClient.GetChangesPeopleAsync(1);
            SearchContainer<ChangesListItem> oldChanges = await TMDbClient.GetChangesPeopleAsync(endDate: DateTime.UtcNow.AddMonths(-1));

            Assert.NotEmpty(page1.Results);
            Assert.NotEmpty(oldChanges.Results);

            // At least one item must be newer in page1
            Assert.Contains(page1.Results, x => oldChanges.Results.All(s => s.Id != x.Id));
        }

        [Fact]
        public async Task TestChangesTvShowsAsync()
        {
            SearchContainer<ChangesListItem> page1 = await TMDbClient.GetChangesTvAsync(1);
            SearchContainer<ChangesListItem> oldChanges = await TMDbClient.GetChangesTvAsync(endDate: DateTime.UtcNow.AddMonths(-1));

            Assert.NotEmpty(page1.Results);
            Assert.NotEmpty(oldChanges.Results);

            // At least one item must be newer in page1
            Assert.Contains(page1.Results, x => oldChanges.Results.All(s => s.Id != x.Id));
        }
    }
}
