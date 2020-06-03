# Glossary


## VST.NET Glossary
<br />
**A** \| **B** \| <a href="#c">C</a> \| **D** \| **E** \| **F** \| <a href="#g">G</a> \| **H** \| **I** \| **J** \| **K** \| **L** \| <a href="#m">M</a> \| **N** \| **O** \| <a href="#p">P</a> \| **Q** \| **R** \| <a href="#s">S</a> \| **T** \| **U** \| <a href="#v">V</a> \| **W** \| **X** \| **Y** \| **Z**
<br />

### C


##### Core Level Implementation, Core Level Plugin

A plugin that just uses the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a> assembly but not the <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a> assembly is a core-level plugin.




### G


##### GAC, Global Assembly Cache

A machine-wide assembly storage location for .NET. An assembly must be signed to be in the GAC. Dependent assemblies will automatically load from the GAC when the assembly is not found in their local folder.




### M


##### Managed Plugin Factory

The Managed Plugin Factory is a class located in the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a> assembly that creates the <a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a> for the managed plugin.

See Also:&nbsp;
<a href="#plugin-command-stub">Plugin Command Stub</a>


##### MIDI

MIDI stands for Musical Instrument Digital Interface and is a standard used to communicate note and control messages to (or from) an audio devicde such as a keyboard or sound module (drum machine).


<a href="http://en.wikipedia.org/wiki/Musical_Instrument_Digital_Interface">Midi on wikipedia</a><a href="http://www.midi.org">Midi Manufacturers Association</a>




### P


##### Plugin Command Proxy

The Plugin Command Proxy is located in the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly.

See Also:&nbsp;
<a href="#plugin-command-stub">Plugin Command Stub</a>


##### Plugin Command Stub

The Plugin Command Stub is a public class that is implemented by the managed plugin that contains all methods for the VST 2.4 interface. These methods are called by the Plugin Command Proxy located in the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly.

See Also:&nbsp;
<a href="#plugin-command-proxy">Plugin Command Proxy</a>


##### Proxy

A Proxy is an object that has the role to receive calls from a client and send those to the Stub. In the process of marshaling the proxy and stub coperate to transmit a call over a boundary.

See Also:&nbsp;
<a href="#stub">Stub</a>



### S


##### Stub

A Stub is an object that has the role to receive calls from a proxy and dispatch those to another object. In the process of marshaling the proxy and stub coperate to transmit a call over a boundary.

See Also:&nbsp;
<a href="#proxy">Proxy</a>



### V


##### VST

VST is a Steinberg owned native C/C++ plugin-host interfacing standard that allows audio plugins to run inside music programs.

See Also:&nbsp;
<a href="#vst.net">VST.NET</a>


##### VST.NET<a href="http://www.codeplex.com/vstnet">VST.NET</a> is an open source Code Library that allows developers to write VST plugins using the .NET framework and any .NET compatible language.

See Also:&nbsp;
<a href="#vst">VST</a>