[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EquipmentStore.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EquipmentStore.Web.App_Start.NinjectWebCommon), "Stop")]

namespace EquipmentStore.Web.App_Start
{
	using EquipmentStore.Common.NinjectModules;
	using EquipmentStore.Web.Infrastructure;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Modules;
	using Ninject.Web.Common;
	using System;
	using System.Web;
	using System.Web.Mvc;

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
			var modules = new INinjectModule[]
			{
				new DalModule(),
				new BllModule(),
				new CoreModule()
			};

			var kernel = new StandardKernel(modules);

			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
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
			DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
		}
	}
}
