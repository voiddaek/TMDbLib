using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLibTests.Helpers;
using VerifyTests;
using VerifyXunit;

namespace TMDbLibTests.JsonHelpers
{
    [UsesVerify]
    public abstract class TestBase
    {
        private VerifySettings VerifySettings { get; }

        protected readonly TestConfig TestConfig;

        protected TMDbClient TMDbClient => TestConfig.Client;

        protected TestBase()
        {
            VerifySettings = new VerifySettings();
            //VerifySettings.AutoVerify();
            VerifySettings.IgnoreProperty<SearchMovie>(x => x.VoteCount, x => x.Popularity);

            JsonSerializerSettings sett = new JsonSerializerSettings();

            WebProxy proxy = null;
            //WebProxy proxy = new WebProxy("http://127.0.0.1:8888");

            TestConfig = new TestConfig(serializer: JsonSerializer.Create(sett), proxy: proxy);
        }

        protected Task Verify<T>(T obj, Action<VerifySettings> configure = null)
        {
            VerifySettings settings = VerifySettings;

            if (configure != null)
            {
                settings = new VerifySettings(VerifySettings);
                configure(settings);
            }

            return Verifier.Verify(obj, settings);
        }
    }
}