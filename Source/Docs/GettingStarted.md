# Getting Started

This will help you to get your first managed Plugin off the ground. In four short
steps you are guided along to produce a "working" Plugin. Although the
function of the Plugin is to pass on audio it receives, it does demonstrate
what components come into play.

The following sections will be mainly focused on the use of the
Framework assembly. But important or interesting information for Core level    implementations (not using the Framework) will be given in a separate section when appropriate.

## Prepare your Project
In [this section](PrepareYourProject))
      you will prepare your Visual Studio Project by setting up a post-build 
      event. The statements in the post-build event will copy and rename the 
      Interop
      assembly as well as the project output assembly, giving it a 
      `.net.dll`
      or a 
      `.net.vstdll`
      extension.
        These naming conventions for the assemblies are necessary to 
      correclty load the Managed Plugin in the (unmanaged) Host.

## Implement the Plugin Command Stub

In 
      <link xlink:href="3feb73bb-72dd-4618-816f-f9f1c46d7f73">this section</link>
      you will create a public class that implements all the VST 2.4 methods. 
      Luckily all the hard work has been done in the
      <token>framework</token> 
      assembly and you just derive from a base class and override one virtual 
      method to create and return the Plugin root object.

<title>Implement the Plugin Root Class</title>
      <content>
        <para>In 
      <link xlink:href="2d6d5838-0551-4404-b5c8-698de8d41aa7">this section</link>
      you will implement a Plugin root object also by deriving from a base class 
      in the 
      <token>framework</token>
      assembly.
      </para>
        <para>The Plugin root object receives all requests for subsequent 
      interfaces, each representing a specific feature or area of the VST 2.4
      specification.

<title>Implement an Audio Processor</title>
      <content>
        <para>Most plugins implement an Audio Processor that deals with the audio
      samples that "flow" through the Plugin. In this section you will setup
      a dummy Audio Processor that does nothing but pass on the receieved 
      audio sample.</para>
        <para>
          <link xlink:href="5e94bd76-fadd-4def-9e1a-261b18a42f0e">This section</link>
      demonstrates how the Plugin implements additional interfaces. All 
      framework interfaces are requested through the Plugin root object.
