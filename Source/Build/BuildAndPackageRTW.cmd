CALL IncludeVsTools.cmd

CALL CleanDir.cmd ..\_SharedAssemblies
CALL DeleteDir.cmd ..\BuildResults

CALL BuildRTW.cmd
CALL PackageRTW.cmd

pause