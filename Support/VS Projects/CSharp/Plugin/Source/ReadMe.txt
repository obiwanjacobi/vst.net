How to build and package VST.NET Visual Studio Templates.

- Open the VstNetProjectTemplates solution in VS2008
- Select from the File menu 'Export Template...'
- Select Project Template for VstNetAudioPlugin and VstNetMidiPlugin and finish the wizard.
- Select Item Template for VstNetItemTemplates and select one file (at a time). 
	Don't select any dependencies and enter the file name (same as the source).
- Repeat previous step for each file in VstNetItemTemplates.
- Copy over the .zip files into the $/vstnet/Support/VS Projects/Deployment/CSharp folder.
- Put the .vscontent and CSharp folder in a zip file and rename the .zip extension to .vsi