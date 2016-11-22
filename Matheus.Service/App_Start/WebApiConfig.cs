using Matheus.Web.Config.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Matheus.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var cors = new EnableCorsAttribute("*", "*", "*");
			// Web API configuration and services
			config.EnableCors(cors);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);


			config.Formatters.Add(new BrowserJsonFormatter()); // return api request as JSON

			config.Filters.Add(new JsonResponseActionFilterAttribute()); // wrap api response

			RegisterJsonFormattingSettings(GlobalConfiguration.Configuration.Formatters.JsonFormatter);
		}

		private static void RegisterJsonFormattingSettings(JsonMediaTypeFormatter jsonFormatter)
		{
			//GlobalConfiguration.Configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

			jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Include;
			jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
		}
	}


	public class BrowserJsonFormatter : JsonMediaTypeFormatter
	{
		public BrowserJsonFormatter()
		{
			this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			this.SerializerSettings.Formatting = Formatting.Indented;
		}

		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			base.SetDefaultContentHeaders(type, headers, mediaType);
			headers.ContentType = new MediaTypeHeaderValue("application/json");
		}
	}







}
