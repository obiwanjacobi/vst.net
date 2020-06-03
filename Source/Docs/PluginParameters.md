# Plugin Parameters

A Plugin Parameter advertizes a runtime variable that controls a part of the workings of the Plugin providing a (slightly) different output when its value is manipulated. Hosts generally provide the following service to a plugin that supports parameters:
&nbsp;<ul><li>Auto-generated Editor UI. 
A form that lists all the Parameters for a Plugin is generated at runtime allowing the user to interact with the Plugin Parameters through this user interface.</li><li>Parameter value automation. 
When a Host is a sequencer they ofter allow the Plugin Parameter values to be stored on an automation track. This provides repeatable and controlled Parameter value changes with (pretty) exact timing.</li><li>Parameter value persistence. 
The current settings of the Plugin Parameter values can be saved along with the rest of the arrangement.</li></ul>&nbsp;
The use of Plugin Parameters is closely linked to <a href="da9d3d7d-c5f5-4d05-99a2-70b020f2cbfb">Programs</a>.



## Framework Parameter Support

The Parameter support <a href="http://www.codeplex.com/vstnet">VST.NET</a> offers goes beyond the VST specification - as well as Program support. VST.NET provides several classes that can be used as is, or extended to allow custom properties and functionality. But first lets see how a Plugin exposes its Parameters.


When a Plugin supports the IVstPluginParameters interface it indicates to the Framework that the Plugin has Parameters. You only have to implement this interface explicitly when you don't support <a href="da9d3d7d-c5f5-4d05-99a2-70b020f2cbfb">Programs</a>. That is because the VstProgram class also implements this interface and the <a href="0aca5a96-16d9-4f8e-a830-202d8bad418a">Interface Manager</a> is hardwired to use that implementation of the `IVstPluginParameters` interface when Programs are supported by the Plugin.


The Plugin Parameters are represented by the VstParameter class. One instance is created for each parameter - although this is not true when supporting <a href="da9d3d7d-c5f5-4d05-99a2-70b020f2cbfb">Programs</a>. To be able to construct a `VstParameter` you need a VstParameterInfo instance that describes the meta information of the parameter. Its name, labels and value range are in the `VstParameterInfo` class. These two classes are the minimum you need to support Parameters for your plugin.
&nbsp;<table><tr><th>![Caution](media/AlertCaution.png) Caution</th></tr><tr><td>
Not all hosts call the VST method to inquire the meta properties of Parameters (represented by `VstParameterInfo`). That type of Hosts will assume your Parameter value range is between 0.0 and 1.0 for all Paremeters. That means that if your Parameter value range is between -1.0 and +1.0 you will not have access to your full range.</td></tr></table>&nbsp;
To transparantly manage the parameter value range depending on the host calling the correct method to retrieve the parameter properties, VST.NET uses an extra object called `VstParameterNormalizationInfo`. This class can be attached to an instance of the `VstParameterInfo` class and will be used to adjust the value of the parameter. Internally the plugin will still see the Parameter value range it expects. But when the Host queries for the Parameter value and it has a `VstParameterNormalizationInfo` object attached to it, its value will be mapped to a range of 0.0 to 1.0. The NormalizationInfo property on the `VstParameterInfo` that contains the instance of the `VstParameterNormalizationInfo` class will be reset (set to null or Nothing) when the Host calls the method to retrieve the parameter properties that specify the parameter's value range. It is recommended that you always attach an `VstParameterNormalizationInfo` instance to every parameter the plugin contains for it will be transparently removed in the case the Host calls for the parameter properties. But if the Host does not, it will serve out a parameter value range that is managable for (almost) all hosts.



## Parameter Categories

A Plugin Parameter can be linked to a Category. Parameter Categories are represented by the VstParameterCategory class, which is no more than a human readable category name. The idea here is that the sub components your Plugin consists of (like oscilator 1, oscilator 2, envelope 1 and envelope 2) all publish their own Parameters linked to their category. Your Plugin implementation of the `IVstPluginParameters` collects these Parameters in one collections to expose to the Host (through the Framework).



## See Also


#### Reference
IVstPluginParameters<br />VstParameter<br />VstParameterCollection<br />VstParameterInfo<br />VstParameterNormalizationInfo<br />VstParameterCategory<br />VstParameterCategoryCollection<br />VstParameterManager<br />

#### Other Resources
<a href="da9d3d7d-c5f5-4d05-99a2-70b020f2cbfb">Plugin Programs</a><br /><a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a><br /><a href="http://obiwanjacobi.blogspot.com/2008/05/vstnet-programs-and-parameters.html">VST.NET Programs and Parameters</a><br />