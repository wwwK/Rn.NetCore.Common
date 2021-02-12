using Rn.NetCore.Common.Services;
using Rn.NetCore.Common.Wrappers;

namespace Rn.NetCore.Common.Factories
{
  public interface IHttpClientFactory
  {
    IHttpClient NewHttpClient();
    IHttpClient NewHttpClient(TimeoutHandler timeoutHandler);
  }

  public class HttpClientFactory : IHttpClientFactory
  {
    public IHttpClient NewHttpClient()
    {
      // TODO: [TESTS] (HttpClientFactory.NewHttpClient) Add tests
      return new HttpClientWrapper();
    }

    public IHttpClient NewHttpClient(TimeoutHandler timeoutHandler)
    {
      // TODO: [TESTS] (HttpClientFactory.NewHttpClient) Add tests
      return new HttpClientWrapper(timeoutHandler);
    }
  }
}
