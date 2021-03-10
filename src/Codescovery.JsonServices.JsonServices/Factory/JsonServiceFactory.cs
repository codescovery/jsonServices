using Codescovery.JsonServices.Interfaces;
using Codescovery.JsonServices.Services;

namespace Codescovery.JsonServices.Factory
{
    public static class JsonServiceFactory
    {
        public static IJsonService Create() => new JsonService();
    }
}