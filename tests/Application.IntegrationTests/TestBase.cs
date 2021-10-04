using System.Threading.Tasks;
using GiraffeMissile.Application.IntegrationTests;
using NUnit.Framework;

namespace GiraffeMissile.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
