# Implement the Plugin Root Class

The IVstPlugin interface is mandatory for all plugin (root object) implementations. Implementing this interface yourself is clearly the most work. The base classes described in the following section implement the interface for you.


The interface contains mostly properties that communicate initial plugin information to the host. But the most important role of the Plugin root object is that it receives interface requests through the IExtensible interface implementation. The `IExtensible` interface is the base interface for the `IVstPlugin` interface.


The Plugin root object is also the only object that gets disposed by the framework through the `IDispose` interface, which is also a base for the `IVstPlugin` interface.


The following sections describe the different options you have when implementing the Plugin root object. Pick one that suites you most.



## Implement IVstPlugin

The first option you have is to implement the IVstPlugin interface yourself (or should this be your last option?). This option provides the most flexibility in how you handle implementing the properties and the `IExtensible` interface. You still have the option to use the <a href="0aca5a96-16d9-4f8e-a830-202d8bad418a">Plugin Interface Manager</a> in your implementation.


The following code snippet shows a basic implementation. You can also refer to the samples to see implementations of this interface. Note that the implementation of the `IExtensible` interface uses the Plugin root object instance to resolve additional implemented interfaces.


**C#**<br />
``` C#
using Jacobi.Vst.Framework;

internal class Plugin : IVstPlugin
{
    private IVstHost _host;

    #region IVstPlugin Members

    private VstProductInfo _productInfo;
    public VstProductInfo ProductInfo
    {
        get
        {
            if (_productInfo == null)
            {
                _productInfo = new VstProductInfo("My Product", "My Vendor", 1000);
            }

            return _productInfo;
        }
    }

    public string Name
    {
        get { return "My Plugin Name"; }
    }

    public Jacobi.Vst.Core.VstPluginCategory Category
    {
        get { return Jacobi.Vst.Core.VstPluginCategory.Unknown; }
    }

    public VstPluginCapabilities Capabilities
    {
        get { return VstPluginCapabilities.None; }
    }

    public int InitialDelay
    {
        get { return 0; }
    }

    // A four character code as integer.
    public int PluginID
    {
        get { return 0; }
    }

    public void Open(IVstHost host)
    {
        _host = host;
    }

    public void Suspend()
    {
    }

    public void Resume()
    {
    }

    #endregion

    #region IExtensibleObject Members

    public bool Supports<T>() where T : class
    {
        return (this is T);
    }

    public T GetInstance<T>() where T : class
    {
        return (this as T);
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
        _host = null;
    }

    #endregion
}
```


## Derive from VstPluginBase

The VstPluginBase class implements the IVstPlugin for you. Most of the properties are set to the value passed into the constructor of this base class and the methods of the interface are all implemented as virtual, thus overridable in your derived class. Another important aspect of using this base class is that it assumes you implement all additional interfaces on the Plugin root object instance itself (a bit like the original VST C++ SDK did). If you want to use the PluginInterfaceManagerBase class you should override the `Supports` and `GetInstance` methods and route those to your instance of the class that derives from `PluginInterfaceManagerBase` class.


The following code example shows you how to use the VstPluginBase class.


**C#**<br />
``` C#
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

class MyPlugin : VstPluginBase, IVstPluginBypass
{
    public MyPlugin()
        : base("My Plugin", 
            new VstProductInfo("My Product", "My Vendor", 1000),
            VstPluginCategory.Synth, 
            VstPluginCapabilities.NoSoundInStop, 
            0, 
            0)  // enter unique plugin ID
    {}

    private bool _bypass;
    public bool Bypass
    {
    	get { return _bypass; }
    	set { _bypass = value; OnBypassChanged(); }
    }

    protected virtual void OnBypassChanged()
    {
    	// TODO: raise event to trigger audio processor
    }

    // other methods...
}
```

Compared to the previous section there is a lot less coding to do. The `VstPluginBase` class implements the `IExtensible` interface in the same way as show in the previous section. The Dispose() method is virtual and can be overriden. Do call the base class for it resets the Host property.



## Derive from VstPluginWithInterfaceManagerBase

The VstPluginWithInterfaceManagerBase not only provides an implementation for the `IVstPlugin` interface it also derives from the PluginInterfaceManagerBase base class. The methods of the `IExtension` interface are mapped to the implementation provided by the `PluginInterfaceManagerBase` class.


This means that all the `CreateXxxx` overrides are implemented on the Plugin root object as shown in the following example.


**C#**<br />
``` C#
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

class MyPlugin : VstPluginWithInterfaceManagerBase
{
    public MyPlugin()
        : base("My Plugin", 
            new VstProductInfo("My Product", "My Vendor", 1000),
            VstPluginCategory.Synth, 
            VstPluginCapabilities.NoSoundInStop, 
            0, 
            0)  // enter unique plugin ID
    {}

    protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
    {
        if (instance == null) return new AudioProcessor(this);

        // base class just returns 'instance'
        return base.CreateAudioProcessor(instance);
    }

    // other methods...
}
```
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Note that the `AudioProcessor` is a class that is not shown in the sample code but is meant to demonstrate how the objects that implement the additional interfaces of <a href="http://www.codeplex.com/vstnet">VST.NET</a> could be managed.</td></tr></table>

## See Also


#### Reference
IVstPlugin<br />IExtensible<br />PluginInterfaceManagerBase<br />VstPluginBase<br />VstPluginWithInterfaceManagerBase<br />

#### Other Resources
<a href="0aca5a96-16d9-4f8e-a830-202d8bad418a">Plugin Interface Manager</a><br /><a href="62feac6e-0c75-4ef8-8703-fb970f81280b">Plugin Root Class</a><br />