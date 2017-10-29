using System.Configuration;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HerbTrans.Infrastructure.Files;
using HerbTrans.Infrastructure.Models;
using HerbTrans.Main;

namespace HerbTrans.Client
{
    public class HerbTransInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.ExtendedNLog).WithAppConfig());

            container.Register(

                Component.For<IParser<Price>>().ImplementedBy<PriceParser>(),
                Component.For<ICsvReader<Price>>().ImplementedBy<PriceReader>(),
                Component.For<IParser<DayRate>>().ImplementedBy<DayRateParser>(),
                Component.For<ICsvReader<DayRate>>().ImplementedBy<DayRateReader>(),
                Component.For<IParser<FileProcess>>().ImplementedBy<HerbProcessParser>(),
                Component.For<ICsvReader<FileProcess>>().ImplementedBy<HerbProcessReader>(),
                Component.For<IFileHelper>().ImplementedBy<FileHelper>(),

                Component.For<IHerbTranService>().ImplementedBy<HerbTranService>()
                    .DependsOn(new { path = ConfigurationManager.AppSettings["ProcessFilePath"] })

                );
        }
    }
}
