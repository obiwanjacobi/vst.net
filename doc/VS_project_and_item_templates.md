VST.NET comes with Visual Studio Project and Item Templates.

When starting a project from scratch use the Project Templates.

**Project Templates**
- Audio Plugin
Creates a fully functional VST.NET plugin project that contains an audio effect (Delay).
- Midi Plugin
Creates a fully functional VST.NET plugin project that contains an midi effect (Gain and Transpose).

The Item Templates are used to create individual VST.NET objects.

**Item Templates**
- PluginCommandStub
The entry object that is created by the interop layer to load your plugin.
- Plugin
The (functional) root object of your plugin.
- AudioProcessor
A class that performs audio processing.
- PluginEditor
A class that manages the custom plugin UI.
- PluginPrograms
A class that manages the plugin programs.

**Install the VST.NET templates into Visual Studio**
- Open Visual Studio (2008/2010/2012) and open the Extension Manager (under the Tools menu).
- Click on the 'Online Gallery' (left).
- Type 'VST.NET' in the search box.
- Select the 'VST.NET project and item templates for VS2008/VS2010' entry.
- Press the 'Download' button.
- Open the download.
- Select the parts of the VST.NET Templates you wish to install (all selected by default).
- Press the 'Next' button - you get a warning that the code is not signed. Press 'Yes'. If you click no you'll abort.
- Press the 'Finish' button and wait for the install to finish (fast).
- Press the 'Close' button.
- Start a new Project (File->New->Project)
- You should see a 'VST.NET' entry in the 'Installed Templates' list.
- Pick the Audio or Midi project type, provide a name and hit Enter.
- Build the project and load the {"[MyProject](MyProject)"}.dll into a DAW to test it.

* If you can't find the templates try loading them directly from the Visual Studio Gallery web site:
[http://visualstudiogallery.msdn.microsoft.com/0fc1a140-269e-4f55-888a-c30577add35a](http://visualstudiogallery.msdn.microsoft.com/0fc1a140-269e-4f55-888a-c30577add35a)

The source for these templates can be found in source control: $/vstnet/Support/VS Projects/CSharp/Plugin/Source

