# VST 3

The thin Interop layer that loads the plugin factory seems to work nicely. **Windows-Only** though.

The managed plugin exposes a public `PluginFactory` derived from the `PluginClassFactory` in Core.

## Issues

- Do not include the [plugin].deps.json in the deployment. Loading will report seeing the same module with different extensions.
- Ijwhost.dll may have to be copied to the [host].exe location. Both the validator.exe and the VST3PluginTestHost.exe did not load the plugin otherwise.
Something is going on with setting the current directory..?
`vsthost.exe` (Hermann Seib) does load the .vst3 plugin correctly without having to copy over Ijwhost.dll...
- Does 64 bit build expects also 64 bit (double) audio processing?? Currently the TestPlugin AudioProcessor is hardcoded to 32 bit.
- The validator.exe points out a few errors:
    - Error: Unit 001: Should be the Root Unit => id should be 000!! [XXXXXXX Failed]
    - Error: All Audio Processing fails.
    - Warning: Parameters Changes - No point at all has been read via IParameterChanges (more...).

## Research

COM (VST3)
https://github.com/dotnet/runtime/blob/master/docs/design/features/COM-activation.md#NET-Framework-Class-COM-Activation
