/*
The MIT License (MIT)

Copyright (c) 2007 - 2019 Microting A/S

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormTrashInspectionBase.Infrastructure.Data.Factories;
using Microting.eFormTrashInspectionBase.Infrastructure.Data;
using Rebus.Bus;
using TrashInspection.Pn.Abstractions;
using TrashInspection.Pn.Installers;
using eFormCore;

namespace TrashInspection.Pn.Services
{
    public class RebusService : IRebusService
    {
        private IBus _bus;
        private IWindsorContainer _container;
        private string _connectionString;
        private string _sdkConnectionString;

        public RebusService()
        {            
        }

        public void Start(string sdkConnectionString, string connectionString, int maxParallelism, int numberOfWorkers)
        {
            _connectionString = connectionString;
            _sdkConnectionString = sdkConnectionString;
            _container = new WindsorContainer();
            _container.Install(
                new RebusHandlerInstaller()
                , new RebusInstaller(connectionString, maxParallelism, numberOfWorkers)
            );

            // We start this and don't use the IEFormCoreService,
            // because that one is singleton and would limit the concurrency 
            Core _core = new Core();
            _core.StartSqlOnly(_sdkConnectionString);

            _container.Register(Component.For<Core>().Instance(_core));
            _container.Register(Component.For<TrashInspectionPnDbContext>().Instance(GetContext()));
            _bus = _container.Resolve<IBus>();
        }

        public IBus GetBus()
        {
            return _bus;
        }
        private TrashInspectionPnDbContext GetContext()
        {
            TrashInspectionPnContextFactory contextFactory = new TrashInspectionPnContextFactory();
            return contextFactory.CreateDbContext(new[] {_connectionString});

        }
    }
}