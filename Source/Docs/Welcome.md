# Welcome

Welcome to <a href="http://www.codeplex.com/vstnet">VST.NET</a>.


<a href="http://www.codeplex.com/vstnet">VST.NET</a> is an open source project hosted on codeplex that allows you to write VST plugins in .NET. There are two levels to interface with the unmanaged Host:
&nbsp;<ul><li>At Core level.</li><li>Use the Framework.</li></ul>&nbsp;
Interfacing at Core level allows a developer to directly use the managed versions of the native VST methods. This approach has minimal overhead but requires intimate knowledge of the VST standard. For adapting existing code to the VST standard, only using Core level can be a valid option.


The framework on the other hand provides the developer with a structured basis on which to build their Plugin. The framework abstracts some of the details of the VST interface away. It also structures the VST standard by grouping functionality in clearly defined (code) interfaces, almost all of which are optional.



## Virtual Studio Technology (VST)

_Text below taken from_<a href="http://en.wikipedia.org/wiki/Virtual_Studio_Technology">wikipedia</a>.


Steinberg's Virtual Studio Technology (VST) is an interface for integrating software audio synthesizer and effect plugins with audio editors and hard-disk recording systems. VST and similar technologies use Digital Signal Processing to simulate traditional recording studio hardware with software. Thousands of plugins exist, both commercial and freeware, and VST is supported by a large number of audio applications. The technology can be licensed from its creator, Steinberg.


VST plugins are generally run within a Digital Audio Workstation, providing the host application with additional functionality. Most VST plugins can be classified as either instruments (VSTi) or effects, although other categories exist. VST plugins generally provide a custom GUI, displaying controls similar to the physical switches and knobs on audio hardware. Some (often older) plugins rely on the host application for their UI.


VST instruments include software emulations of well-known hardware synthesizer devices and samplers, emulating the look of the original equipment and its sonic characteristics. This enables VSTi users to use virtual versions of devices that may be otherwise difficult to obtain.


VST instruments require notes to be sent via MIDI in order to output audio, while effect plugins process audio data. MIDI messages can often also be used to control parameters of both instrument and effect plugins. Most host applications allow the audio output from one VST to be routed to the audio input of another VST (known as chaining). For example, output of a VST synthesizer can be sent to a VST reverb effect for further processing.


With appropriate hardware and drivers, such as a sound card that supports ASIO, VST plugins can be used in real-time. ASIO bypasses Windows' slower audio engine, offering much lower latency.



## Advantages of using VST.NET.
&nbsp;<ul><li><a href="http://www.codeplex.com/vstnet">VST.NET</a> is easier to learn than the native VST SDK API.</li><li>You can program VST plugins in any .NET language.</li><li><a href="http://www.codeplex.com/vstnet">VST.NET</a> provides a framework that simplifies and clarifies the VST interfacing options.</li><li>It takes far less time to develop a managed <a href="http://www.codeplex.com/vstnet">VST.NET</a> plugin than to develop a C/C++ native VST plugin.</li><li><a href="http://www.codeplex.com/vstnet">VST.NET</a> provides you with good documentation and samples.</li><li>Much less time is spent on programming 'plumbing'.</li><li>Your existing native VST knowledge still applies.</li><li>Managed code provides a robust basis to work in.</li></ul>

## See Also


#### Other Resources
<a href="http://www.codeplex.com/vstnet">VST.NET on www.codeplex.com</a><br /><a href="http://www.steinberg.net/en/company/3rd_party_developer.html">Steinberg VST SDK</a><br />