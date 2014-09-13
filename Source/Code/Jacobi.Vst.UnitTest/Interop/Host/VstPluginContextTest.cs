using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Vst.Interop.Host;
using System.Reflection;

namespace Jacobi.Vst.UnitTest.Interop.Host
{
    [TestClass]
    public class VstPluginContextTest
    {
        public TestContext TestContext;

        [WorkItem(10484)]
        [WorkItem(8488)]
        [TestMethod]
        [ExpectedException(typeof(EntryPointNotFoundException))]
        public void Create_InvalidPluginFile_ThrowsExpectedExceptionWithResourceString()
        {
            var hostCmdStub = new StubHostCommandStub();
            var notaPluginFile = Assembly.GetExecutingAssembly().Location;

            VstPluginContext.Create(notaPluginFile, hostCmdStub);
        }
    }
}
