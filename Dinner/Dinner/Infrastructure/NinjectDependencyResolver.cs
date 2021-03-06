﻿using System;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Dinner.Models;

namespace Dinner.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings() {
            //Mock<IUserRepository> mock = new Mock<IUserRepository>();
            //mock.Setup(m => m.Users).Returns(new List<User> {
            //    new User { UserId = 1, Name = "Tom Hanks", Email = "tom@goole.com" },
            //    new User { UserId = 2, Name = "John Smith", Email = "John.Smith@google.com"},
            //    new User { UserId = 3, Name = "Daniel Chen", Email = "Jun.Chen@autodesk.com"}
            //});

            //kernel.Bind<IUserRepository>().ToConstant(mock.Object);

            kernel.Bind<IUserRepository>().To<EFUserRepository>();
        }
    }
}