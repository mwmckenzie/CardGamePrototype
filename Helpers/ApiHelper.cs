using System.Net;
using CaptrsCardGamePrototype.Pages;

namespace CaptrsCardGamePrototype.Helpers; 

public class ApiHelper {

    private HttpClient Http = new();
    
    string method = "GET";
    string requestBody = "";
    List<ApiTestPage.RequestHeader> requestHeaders = new List<ApiTestPage.RequestHeader>();

    HttpStatusCode? responseStatusCode;
    
    string responseHeaders;
    
    public string uri { get; set; }
    public string responseBody { get; set; }
    
    public async Task DoRequest()
    {
        responseStatusCode = null;

        try
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri(uri),
                Content = string.IsNullOrEmpty(requestBody)
                    ? null
                    : new StringContent(requestBody)
            };

            foreach (var header in requestHeaders)
            {
                // StringContent automatically adds its own Content-Type header with default value "text/plain"
                // If the developer is trying to specify a content type explicitly, we need to replace the default value,
                // rather than adding a second Content-Type header.
                if (header.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase) && requestMessage.Content != null)
                {
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(header.Value);
                    continue;
                }

                if (!requestMessage.Headers.TryAddWithoutValidation(header.Name, header.Value))
                {
                    requestMessage.Content?.Headers.TryAddWithoutValidation(header.Name, header.Value);
                }
            }

            var response = await Http.SendAsync(requestMessage);
            responseStatusCode = response.StatusCode;
            responseBody = await response.Content.ReadAsStringAsync();
            var allHeaders = response.Headers.Concat(response.Content?.Headers
                ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>());
            responseHeaders = string.Join(
                Environment.NewLine,
                allHeaders.Select(pair => $"{pair.Key}: {pair.Value.First()}").ToArray());
        }
        catch (Exception ex)
        {
            if (ex is AggregateException)
            {
                ex = ex.InnerException;
            }
            responseStatusCode = HttpStatusCode.SeeOther;
            responseBody = ex.Message + Environment.NewLine + ex.StackTrace;
        }
    }

    void AddHeader()
        => requestHeaders.Add(new ApiTestPage.RequestHeader());

    void RemoveHeader(ApiTestPage.RequestHeader header)
        => requestHeaders.Remove(header);

    class RequestHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}