CALL Create_PackagesDir.cmd

cd Code
CALL CleanBuildResults.cmd
CALL BuildRTM.cmd
CALL PackageCopyAll.cmd
cd ..

cd Samples
CALL CleanBuildResults.cmd
CALL BuildRTM.cmd
CALL PackageCopyAll.cmd
cd ..

del *.zip
7za.exe a "VST.NET RTM.zip" .\_Packages\*

pause