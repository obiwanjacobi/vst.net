using FluentAssertions;
using Jacobi.Vst.Host.Interop;
using System;
using System.Reflection;

namespace Jacobi.Vst.UnitTest.Interop.Host;

public class VstPluginContextTest
{
    //[WorkItem(10484)]
    //[WorkItem(8488)]
    [Fact]
    public void Create_InvalidPluginFile_ThrowsExpectedException()
    {
        var hostCmdStub = new StubHostCommandStub();
        var notaPluginFile = Assembly.GetExecutingAssembly().Location;

        Action target = () => VstPluginContext.Create(notaPluginFile, hostCmdStub);

        target.Should().Throw<EntryPointNotFoundException>();
    }
}
