using System;
using System.Collections.Generic;
using System.ComponentModel;
using DIContainer;
using DIContainer.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DIContainer.Tests
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ElementAlreadyExistsException))]
        public void ClassAlreadyExistsTestMethod()
        {
            var container = new Container();

            container.Register(typeof(Toyota));
            container.Register(typeof(ICar), typeof(Toyota));
        }

        [TestMethod]
        [ExpectedException(typeof(InterfaceNotImplementedException))]
        public void InterfaceNotImplementedTestMethod()
        {
            var container = new Container();
            container.Register(typeof(IPlane), typeof(Toyota));
        }

        [TestMethod]
        public void SimpleRegisterTestMethod()
        {
            var container = new Container();
            container.Register(typeof(ICar), typeof(BMW));
            var expected = new BMW();

            Assert.AreEqual(expected.GetType(), container.Resolve(typeof(ICar)).GetType());
        }

        [TestMethod]
        public void ElementCountTestMethod()
        {
            var container = new Container();
            container.Register(typeof(ICar), typeof(BMW));
            container.Register(typeof(Toyota));
            Assert.AreEqual(2, container.Count);
        }

        [TestMethod]
        public void RegistrationWithParamsTestMethod()
        {
            var container = new Container();
            container.Register(typeof(Toyota));
            object[] parameters = {"Corolla", 2008};
            var expected = new Toyota();
            Assert.AreEqual(expected.GetType(), container.Resolve(typeof(Toyota), parameters).GetType());
        }

        [TestMethod]
        public void RegistrationWithLabelsTestMethod()
        {
            var container = new Container();
            container.Register(typeof(ICar), typeof(BMW), "BMW X5");
            container.Register(typeof(ICar), typeof(Toyota), "Toyota Celica");
            var bmwExpected = new BMW();
            var toyotaExpected = new Toyota();
            var bmwActual = container.Resolve(typeof(ICar), "BMW X5");
            var toyotaActual = container.Resolve(typeof(ICar), "Toyota Celica");
            Assert.AreEqual(2, container.Count);
            Assert.AreEqual(bmwExpected.GetType(), bmwActual.GetType());
            Assert.AreEqual(toyotaExpected.GetType(), toyotaActual.GetType());
        }
    }
}
