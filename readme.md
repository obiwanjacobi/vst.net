# `VST.NET` 2

## New Version: `VST.NET` 2

`VST.NET` 2 builds on the basis of `VST.NET` (1) and is completely built with .NET 5.
The new version is still Windows-only and supports the Steinberg VST 2.x API.

This means that `VST.NET` (1) is phasing out and will not receive any more updates.
If you are looking for the old `VST.NET` (1) code, its in the branch called [`vstnet1`](https://github.com/obiwanjacobi/vst.net/tree/vstnet1).
Of course you can still ask questions about `VST.NET` (1), but no feature requests or bug fixes will be applied.

## What is `VST.NET`

VST stands for Virtual Studio Technology and is an API designed by Steinberg that allows Audio and Midi plugin to work together in a Digital Audio Workstation (DAW) host application.

`VST.NET` allows VST Plugin developers to write Plugins for the Steinberg VST 2.x API. The interop layer makes the transition between the C++ and C# smooth and easy.
The Framework built on top of the interop layer provides a clear and structured architecture accelerating development considerably.

`VST.NET` also allows developers to write a managed VST2 Host application. The VstPluginContext class (Host.Interop) allows you to load and communicate with unmanaged (and managed) VST Plugins. At this time there is no Framework for Host applications. But any ideas on this are welcome.

## Documentation

Still a work in progress, but [here](https://obiwanjacobi.github.io/vst.net/index.html) they are.

---

## Community

### Discord

![](docs/_old/media/discord-logo.png)

Discuss on the `VST.NET` Discord Server: https://discord.gg/QyZqQDk

Anyone can join and do not hesitate to ask a question or start a discussion.

### Facebook

![](docs/_old/media/Home_facebook_logo_48x48.jpg)

There is a Facebook page [here](http://www.facebook.com/pages/Virtual-Studio-Technology-for-NET/150408134989174).

### Contribute

If you have a great idea for a new feature, have a suggestion or have found a bug, please [create an issue on github](https://github.com/obiwanjacobi/vst.net/issues). Use the appropriate template.

---

## Donations

Yes please!

It is very much appreciated if you ...
[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=HTE6LFLSC8RPL&lc=US&item_name=Jacobi%20Software&item_number=VST%2eNET&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted)

Or with [Ko-Fi](https://ko-fi.com/obiwanjacobi) (requires a Ko-Fi account)

---

## License

[LGPL Version 2.1](license.md)

---

![](docs/_old/media/Home_VSTLogoAlpha92x54.png) 

VST is a trademark of Steinberg Media Technologies GmbH.
