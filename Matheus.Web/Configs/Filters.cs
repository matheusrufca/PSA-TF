using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Matheus.Web.Config.Filters
{

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

	public class JsonResponseActionFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(HttpActionExecutedContext context)
		{
			ObjectContent content;
			JsonResponse<object> jsonResponse = null;

			try
			{

				jsonResponse = this.BuildApiResponse(context.Response);

				try
				{
					content = context.Response.Content as ObjectContent;

					if (content != null)
					{
						content.Value = jsonResponse;
					}
				}
				catch (Exception ex)
				{
					context.Response.Content = new ObjectContent<JsonResponse<object>>(jsonResponse, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				}
			}
			catch (Exception ex) { }
		}



		private JsonResponse<object> BuildApiResponse(HttpResponseMessage response)
		{
			JsonResponse<object> jsonResponse = null; ;
			ObjectContent content;
			List<string> errorDetailList = new List<string>();
			string errorDetail = String.Empty;

			try
			{
				content = response.Content as ObjectContent;

				jsonResponse = new JsonResponse<object>()
				{
					StatusCode = (int)response.StatusCode,
					Result = content.Value,
					StatusMessage = response.ReasonPhrase,
					ErrorData = String.IsNullOrWhiteSpace(errorDetail) ? null : errorDetail
				};
			}
			catch (Exception ex)
			{
				jsonResponse = new JsonResponse<object>()
				{
					StatusCode = (int)response.StatusCode,
					StatusMessage = response.ReasonPhrase,
					ErrorData = ex.Message
				};
			}

			return jsonResponse;
		}
	}


	[DataContract]
	public class JsonResponse<T> where T : class
	{
		//[IgnoreDataMember]
		//public string Version { get { return "1"; } }

		[DataMember(IsRequired = true)]
		public int StatusCode { get; set; }

		[DataMember]
		public string StatusMessage { get; set; }


		[DataMember(Name = "data")]
		public T Result { get; set; }



		[DataMember(EmitDefaultValue = false)]

		public string ErrorData { get; set; }


		public JsonResponse() { }

		public JsonResponse(HttpStatusCode statusCode, T result = null, string statusMessage = null)
		{
			StatusCode = (int)statusCode;
			StatusMessage = statusMessage;
			Result = result;
		}
	}
}