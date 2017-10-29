using Castle.Core.Logging;
using Castle.Windsor;
using Castle.Windsor.Installer;
using HerbTrans.Main;

namespace HerbTrans.Client
{
    public class HerbTransProgram
    {
        private readonly WindsorContainer _container;
        private ILogger _logger = NullLogger.Instance;
        public HerbTransProgram()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _logger = _container.Resolve<ILogger>();

            _logger.Info("Calling HerbTranService.");
            var service = _container.Resolve<IHerbTranService>();
            service.Execute();
        }
    }
}
