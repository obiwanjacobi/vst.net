How to build and package VST.NET Visual Studio Templates.

- Open the VstNetProjectTemplates solution in VS2019
- Select from the Project menu 'Export Template...'
- Select Project Template for VstNetAudioPlugin and VstNetMidiPlugin and finish the wizard.
- Copy over the .zip files into the $/vstnet/..??../Deployment/CSharp folder.
- Put the .vscontent and CSharp folder in a zip file and rename the .zip extension to .vsi