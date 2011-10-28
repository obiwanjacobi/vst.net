CALL Create_PackagesDir.cmd

cd Code
CALL CleanAll.cmd
CALL BuildRTM.cmd
CALL PackageCopyRTM.cmd
cd ..

cd Samples

cd ..

del *.zip
7za.exe a "VST.NET RTM.zip" .\_Packages\*

pause