using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace SimpleHttpClient
{
	public class HttpClientHelper {

		public HttpClientHelper(){}

		public HttpStatusCode HttpRequest(String url, List<KeyValuePair<String, String>> postParam, out String responseResult) {
			responseResult = null;
			var httpClient = new HttpClient();
			httpClient.Timeout = TimeSpan.FromSeconds(20);

			var httpContent = new FormUrlEncodedContent(postParam);
			var responce = httpClient.PostAsync(url, httpContent).Result;

			if (responce.StatusCode == HttpStatusCode.OK) {
				responseResult = responce.Content.ReadAsStringAsync ().Result;
				return responce.StatusCode;
			} else {
				return responce.StatusCode;
			}
		}

	}
}

