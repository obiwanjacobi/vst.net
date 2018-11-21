# VST.NET Programs and Parameters

This post discusses how Plugin Programs and Parameters are implemented in VST.NET.

A VST Plugin can support parameters. Information about these parameters is communicated back to the Host for it to display in a generic Plugin parameter dialog that is dynamically constructed by the Host based on the parameter (meta) information returned by the Plugin. Even when the Plugin provides its own editor UI the Host still needs to know about these Parameters, for instance to automate them.

A program is a named set of all parameters your Plugin supports. Multiple programs allow the user to make presets with parameter settings and allow him/her to switch quickly between them.

## Parameter Meta Information
VST.NET devides a parameter into two parts. There is a `VstParameterInfo` instance for each parameter in the Plugin. This class contains the parameter meta information that describes the value range of the parameter and its labels. The parameter meta information is also communicated to the host when it calls `GetParameterProperties` on the Plugin. (The Framework fills the structure this method expects based on the `IVstPluginParameters` interface and the `VstParameterInfo` instances along with `VstParameterCategory` instances.) There is only one `VstParameterInfo` instance alive for each parameter in the Plugin. The number of `VstParameterInfo` instances is equal to the number of parameters the plugin supports, it has no relation to the number of programs the Plugin supports.

The other part of a parameter is where its real value is stored. The `VstParameter` class represents the value-part of a parameter. It references the `VstParameterInfo` to be able to get to its own meta data. The number of `VstParameter` instance is a multiplication of the number of parameters of the Plugin, times the number of programs. As said earlier, each program contains a full set of all the Plugin parameters. So each VstParameter in a single program references to a unique `VstParameterInfo` instance.

So we have multitple `VstParameter` instances (one in each program) referencing the same `VstParameterInfo` instance. One of those `VstParameter` instances has the 'active' or 'current' value for the parameter (the instance that lives in the active/current program). We've introduced another class to manage those `VstParameter` instance and represent the active/currentvalue. The `VstParameterManager` is part of `VstParameterInfo` and is hooked onto each `VstParameter` instance representing that parameter (definition). The `VstParameterManager` is (usually) referenced by the Plugin Component (for instance an oscilator) that works with the parameter or the component subscribes to its events.

The following picture shows the multiple `VstParamater` instances that all reference the same `VstParameterInfo` meta data instance. A `VstParameterManager` that keeps tabs on all the `VstParameter` instances and communicates the active/current parameter value to the Plugin Component.

![](media/Parameter%20Objects.PNG)

## Parameter Creation
The idea is that all Plugin (sub-)components create the `VstParameterInfo` instances for their own parameters. The Plugin should 'add these up' to present a complete list to the host through the `IVstPluginParameters` interface.

The following figure shows the object interaction between a Plugin Component and the `VstParameterInfo` and `VstParameterManager` instances. The Plugin Component creates a `VstParameterInfo` instance for each parameter it requires and populate (setting) its properties. Typically the Plugin Component maintains a reference to the `VstParameterManager` it also creates for a specific parameter to be able to work with the active/current parameter value. Note that the `VstParameterManager` is part of the parameter meta information on `VstParameterInfo`.

![](media/Parameter%20Object%20Creation%20OID.PNG)

When the Plugin Component is done with the `VstParameterInfo` construction for parameters it publishes these instance internally to allow the Plugin to collect all the parameter defintions of all its sub components (oscilators, filters, etc.).

Because the Plugin Component creates both, it has a chance to instantiate a derived type with custom functionality. To register extra parameter meta data, derive a class from `VstParameterInfo` and add extra properties and to modify the behavior of the `VstParameterManager` you can also derive a custom class and assign that instance to the `ParameterManager` property on `VstParameterInfo`.

The following figure shows the object interaction that takes place when the programs create their `VstParameter` instances. Ideally there is a central class that contains the knowledge of all parameters in the Plugin and has a fill method to populate a collection with all `VstParameter` instances for all parameters.

![](media/Parameter%20Creation%20OID.PNG)

The `VstParameter` instance is always constructed on an instance of `VstParameterInfo`. The `VstParameter` object will initialize its value with the default value listed in the meta data and allow the `VstParameterManager` to subscribe to the newly created `VstParameter` instance.

## Program Activation
The following figure shows a situation were 2 programs contain n parameters. For the first parameter (Parameter 1) the `VstParameterInfo` and `VstParameterManager` objects are displayed. Of course every `VstParameter` in a `VstProgram` has its own `VstParameterInfo` and `VstParameterManager` instance.

![](media/Program%20Parameter%20Objects.PNG)

But what happens when the user changes programs? The whole set of parameter values should be switched from one program to another. VST.NET defines the `IActivatable` interface that allows an object to be activated or deactivated. The `VstParameter` class implements this interface (as does the `VstParameterCollection`). So when one `VstProgram` is switched to another `VstProgram` instance, all the `VstParameter` instances are deactivated on the first and activated on the second. The `VstParameterManager` for each parameter gets notified of this deactivation and activation and is able to grab the new active/current value and notify the Plugin Component.

The following figure displays the object interaction for (de)activation of a `VstProgram`.

![](media/Program%20and%20Parameter%20(de)Activation%20OID.PNG)

The notifacation to the PluginComponent is not shown here but an event is raised each time the active/current parameter value changes on the `VstParameterManager`.

## Wrapping up
I hope this post has explained to you how the Programs and Parameters work in VST.NET. The seperation between running values of parameters and parameter (meta) definition is a key concept to understand what is going on. The parameter manager takes a lot of work out of your hands and keeps track for you on the active/current parameter value your Plugin Component can work with directly.