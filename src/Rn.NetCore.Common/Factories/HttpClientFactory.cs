using Rn.NetCore.Common.Handlers;
using Rn.NetCore.Common.Services;
using Rn.NetCore.Common.Wrappers;

namespace Rn.NetCore.Common.Factories
{
  public interface IHttpClientFactory
  {
    IHttpClient CreateHttpClient();
    IHttpClient CreateHttpClient(TimeoutHandler timeoutHandler);
  }

  public class HttpClientFactory : IHttpClientFactory
  {
    public IHttpClient CreateHttpClient()
    {
      // TODO: [TESTS] (HttpClientFactory.CreateHttpClient) Add tests
      return new HttpClientWrapper();
    }

    public IHttpClient CreateHttpClient(TimeoutHandler timeoutHandler)
    {
      // TODO: [TESTS] (HttpClientFactory.CreateHttpClient) Add tests
      return new HttpClientWrapper(timeoutHandler);
    }
  }
}
