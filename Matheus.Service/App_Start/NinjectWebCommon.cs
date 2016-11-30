using Matheus.DAL;
using Matheus.DAL.Models;
using Matheus.Repository;
using Matheus.Repository.IRepositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Matheus.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Matheus.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Matheus.Web.App_Start
{
	using AutoMapper;
	using Configs.Mappings;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Common;
	using System;
	using System.Web;
	using System.Web.Http;

	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

				RegisterServices(kernel);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			// Em qualquer parte da aplicação que houver um construtor que
			// peça por uma instância da classe ApplicationDbContext, o Ninject se
			// encarregará de todo o processo
			kernel.Bind<EFDataContext>().ToSelf().InRequestScope();
			kernel.Bind<IUnitOfWork>().To<EFDataContext>().InRequestScope();
			kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));
			kernel.Bind<ICarRepository>().To<CarRepository>();
			kernel.Bind<IFuelSupplyRepository>().To<FuelSupplyRepository>();
			kernel.Bind<IFuelTypeRepository>().To<FuelTypeRepository>();

			// Da mesma forma, quando um construtor pedir por uma instância de uma
			// classe que implemente a interface IPessoaRepositorio, o Ninject 
			// automaticamente criará uma instância da classe PessoaRepositorio
			// e passará como parâmetro

			AutoMapperConfig.Register();
			kernel.Bind<IMapper>().ToConstant(Mapper.Instance);

			// O método InRequestScope() diz que uma instância das referidas classes
			// será criada para cada requisição feita, normalmente é a opção que
			// oferece a melhor performance



		}
	}
}
