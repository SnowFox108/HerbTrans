using System.Configuration;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HerbTrans.Infrastructure.Files;
using HerbTrans.Infrastructure.Models;
using HerbTrans.Main;
using HerbTrans.PricePicker;

namespace HerbTrans.Client
{
    public class HerbTransInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.ExtendedNLog).WithAppConfig());
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));

            container.Register(

                Component.For<IParser<Price>>().ImplementedBy<PriceParser>(),
                Component.For<ICsvReader<Price>>().ImplementedBy<PriceReader>(),
                Component.For<IParser<DayRate>>().ImplementedBy<DayRateParser>(),
                Component.For<ICsvReader<DayRate>>().ImplementedBy<DayRateReader>(),
                Component.For<IParser<FileProcess>>().ImplementedBy<HerbProcessParser>(),
                Component.For<ICsvReader<FileProcess>>().ImplementedBy<HerbProcessReader>(),
                Component.For<IFileHelper>().ImplementedBy<FileHelper>(),

                Component.For<ICategoryPicker>().ImplementedBy<BeautyCategoryPicker>().Named("BeautyCategoryPicker"),
                Component.For<ICategoryPicker>().ImplementedBy<HerbCategoryPicker>().Named("HerbCategoryPicker"),
                Component.For<ICategoryPicker>().ImplementedBy<MedicineCategoryPicker>().Named("MedicineCategoryPicker"),
                Component.For<ICategoryPicker>().ImplementedBy<FreeCategoryPicker>().Named("FreeCategoryPicker"),
                
                Component.For<IRemainingPicker>().ImplementedBy<RemainingPicker>(),

                Component.For<IHerbTranService>().ImplementedBy<HerbTranService>()
                    .DependsOn(new {path = ConfigurationManager.AppSettings["ProcessFilePath"]}),
                Component.For<IOutputService>().ImplementedBy<OutputService>(),

                Component.For<IDistributor>().ImplementedBy<Distributor>()

            );
        }
    }
}
