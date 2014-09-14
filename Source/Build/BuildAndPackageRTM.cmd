CALL Create_PackagesDir.cmd
CALL IncludeVsTools.cmd

cd Code
CALL CleanBuildResults.cmd
CALL BuildRTM.cmd
CALL PackageCopyAll.cmd
cd ..

REM cd Samples
REM CALL CleanBuildResults.cmd
REM CALL BuildRTM.cmd
REM CALL PackageCopyAll.cmd
REM cd ..

del *.zip
7za.exe a "VST.NET RTM.zip" .\_Packages\*

pause