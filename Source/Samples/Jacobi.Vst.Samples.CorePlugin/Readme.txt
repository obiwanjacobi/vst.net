This sample contains code for both WinForms (.NET 2.0) and WPF (.NET 3.0/3.5)

Switch implementation to WinForms:
- Open the Project properties and select (on the Application tab) Target Framework: .NET Framework 2.0 (optional).
- Set the Build Action to None (file properties) for the EditorControl.xaml and EditorControl.xaml.cs files.
- Set the Build Action to None for the WpfControlWrapper.cs file.
- Set the Build Action to Compile for the EditorControl.cs and EditorControl.Designer.cs files and to Embedded Resource for the EditorControl.resx file.
- Open the PluginCommandStub.cs file and comment-out the line that declares the WpfControlWrapper and comment-in the line that declares the WinFormsWrapper for the _editorCtrl member.
- Build the project.

Switch implementation to Wpf:
- Open the Project properties and select (on the Application tab) Target Framework: .NET Framework 3.5.
- Set the Build Action to None (file properties) for the EditorControl.cs, EditorControl.Designer.cs and EditorControl.resx files.
- Set the Build Action to Page for the EditorControl.xaml file and to Compile for the EditorControl.xaml.cs file.
- Set the Build Action to Compile for the WpfControlWrapper.cs file.
- Open the PluginCommandStub.cs file and comment-in the line that declares the WpfControlWrapper and comment-out the line that declares the WinFormsWrapper for the _editorCtrl member.
- Build the project.

!!Note that the log (listbox on form) does not work for the WPF version.