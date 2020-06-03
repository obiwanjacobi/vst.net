# Plugin Persistence

In order for a user to have all its settings as they were when the Host application saves the work, each Plugin has to exposes in some way its internal settings. One of the easiest ways to do that is to support Plugin <a href="c3df31da-acf5-4f57-8178-c00b1bc545ba">Parameters</a> (and <a href="da9d3d7d-c5f5-4d05-99a2-70b020f2cbfb">Programs</a>). This provides the Host with a structured way to extract the Plugin settings (Parameters) and save them into the file. When the file is loaded into the Host, each Plugin receives the original Parameter values the song (or other work) was saved with.


But exposing Parameters is not always possible or does not always represent all of the Plugin's internal state. In those cases the Plugin must be in charge of (how) the data that is saved and loaded. When a Plugin implements the IVstPluginPersistence interface it tells the Host it wants to be in charge of serializing its internal state into a 'chunk' of memory.


There are two modes of operation. A Host can ask for either a single preset (the current/active one) or for a whole bank of presets (all Programs). This distinction is abstracted away by the VST.NET Framework from the Plugin developer.



## Framework Support

When a Plugin supports the IVstPluginPersistence interface it is called when a Host wants to save the Plugin state to a file or when the file is reloaded that contains a 'chunk' from the Plugin. The Framework handles the preset or bank modes in how it fills the `VstProgramCollection` method parameter passed to the `ReadPrograms` and `WritePrograms` methods. The implementation of these methods should simply look at the `VstProgram` collection passed into the method and serialize or deserialize those programs into or from the specified `Stream`.


The `CanLoadChunk` method is called just before a 'chunk' is sent to the Plugin. The Host has recorded the information on the Plugin version when it was saved. Based on this information the Plugin can decide to abort or continue with loading the Plugin data.


The Framework provides a base class for implementing the `IVstPluginPersistence` interface. The VstPluginPersistenceBase uses the VstProgramWriter and VstProgramReaderBase classes. The `VstProgramWriter` can be used directly (without deriving from it) and writes the Program and Parameter information to a stream. Note that the objects are not serialized, only their key property values are. That is why the counterpart of the writer class is abstract. The class that derives from the `VstProgramReaderBase` class implements the abstract method to create a Program with all of its Parameters. This goes back to the design principle that a Program is typically created from a Factory that aggregates all the Parameters used by all the sub-components of the Plugin. The `VstProgramReaderBase` class just performs a lookup base on the Parameter's name to set its value. And this also explains why the `VstPluginPersistenceBase` class is also abstract because it needs an implementation of the `VstProgramReaderBase` class. Refer to the Delay sample for an example of an implementation of a `VstPluginPersistenceBase` class and a `VstProgramReaderBase` class.
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Be aware of the amount of data you write into the (chunk) stream. Some Hosts do maintain maximum limits for the amount of data that can come from a Plugin.</td></tr></table>

## See Also


#### Reference
IVstPluginPersistence<br />VstPluginPersistenceBase<br />VstProgramWriter<br />VstProgramReaderBase<br />

#### Other Resources
<a href="c3df31da-acf5-4f57-8178-c00b1bc545ba">Plugin Parameters</a><br /><a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a><br /><a href="http://obiwanjacobi.blogspot.com/2008/07/vstnet-plugin-persistence.html">VST.NET Persistence</a><br />