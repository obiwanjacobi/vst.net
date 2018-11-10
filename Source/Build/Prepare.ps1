$buildDir = Split-Path -Path $PSCommandPath
$sourcesDir = Split-Path -Path $buildDir

Function Get-Folder($description)
{
    [System.Reflection.Assembly]::LoadWithPartialName("System.windows.forms") | Out-Null
    
    $OpenFileDialog = New-Object System.Windows.Forms.FolderBrowserDialog
    $OpenFileDialog.Description = $description
    $OpenFileDialog.ShowDialog() | Out-Null
    return $OpenFileDialog.SelectedPath
}




Function SetupVSTFiles ()
{
    $vstHeaderDir = Join-Path -Path $sourcesDir -ChildPath "Code\Jacobi.Vst.Interop\_vst\"

    if(!(Test-Path -Path $vstHeaderDir)){
        Write-Host "Creating directory $vstHeaderDir"

        New-Item -ItemType directory -Path $vstHeaderDir | Out-Null
    }

    $aeffect_h_target = Join-Path -Path $vstHeaderDir -ChildPath "aeffect.h"
    $aeffectx_h_target = Join-Path -Path $vstHeaderDir -ChildPath "aeffectx.h"

    if((Test-Path -Path $aeffect_h_target) -and (Test-Path -Path $aeffectx_h_target)) {
        Write-Host "Both aeffect.h and aeffectx.h exist; no VST SDK setup necessary"
        return;
    }
    
    $vstSDK = Get-Folder("Select the VST2 SDK directory")

    if (!$vstSDK) { 
        [System.Windows.Forms.MessageBox]::Show("No VST2 SDK folder selected, aborting", "Error", 0)
        exit
    }

    $aeffect_h_source = Join-Path -Path $vstSDK -ChildPath "pluginterfaces\vst2.x\aeffect.h"
    $aeffectx_h_source = Join-Path -Path $vstSDK -ChildPath "pluginterfaces\vst2.x\aeffectx.h"

    if (!(Test-Path $aeffect_h_source)) { 
        [System.Windows.Forms.MessageBox]::Show("The file ${aeffect_h_source} was not found, aborting", "Error", 0)
        exit
    }

    if (!(Test-Path $aeffectx_h_source)) { 
        [System.Windows.Forms.MessageBox]::Show("The file ${aeffectx_h_source} was not found, aborting", "Error", 0)
        exit
    }

    Copy-Item -Path $aeffect_h_source -Destination $aeffect_h_target
    Copy-Item -Path $aeffectx_h_source -Destination $aeffectx_h_target
}

Function AdjustProjects ($project, $clr) {
    $projectFolder = Join-Path -Path $sourcesDir -ChildPath "Code\${project}"

    
    $prefix = Join-Path -Path $projectFolder -ChildPath "${project}.${clr}";

    $project = "${prefix}.csproj"

    if (!(Test-Path $project)) { 
        # It's probably the .vcxproj
        $project = "${prefix}.vcxproj"
    }

    if (!(Test-Path $project)) { 
        Write-Host "Neither a .vcxproj nor .csproj found in ${projectFolder}, aborting"
    }
    
    [xml]$myXML = Get-Content $project

    foreach ($v in $myXML.Project.PropertyGroup)
    {
        if ($v.SignAssembly) {
               $v.SignAssembly = "False"
        }

        if ($v.AssemblyOriginatorKeyFile) {
               $v.AssemblyOriginatorKeyFile = ""
        }

        foreach ($c in $v.ClCompile) {
            if ($c.HasAttribute("Include") -and $c.GetAttribute("Include") -like "*AssemblyInfo.General.cpp*") {
                $c.parentNode.RemoveChild($c)
            }
        }

        if ($v.LinkKeyFile) {
            $v.parentNode.RemoveChild($v)
        }

           }

    foreach ($v in $myXML.Project.ItemGroup)
    {
        foreach ($c in $v.Compile) {
            if ($c.HasAttribute("Include") -and $c.GetAttribute("Include") -like "*AssemblyInfo.General.c*") {
                $c.parentNode.RemoveChild($c)
            }
        
        }

        foreach ($c in $v.ClCompile) {
            if ($c.HasAttribute("Include") -and $c.GetAttribute("Include") -like "*AssemblyInfo.General.c*") {
                $c.parentNode.RemoveChild($c)
            }
        
        }

        
         foreach ($c in $v.LinkKeyFile) {
            if ($c.HasAttribute("Include") -and $c.GetAttribute("Include") -like "*Jacobi.snk*") {
                $c.parentNode.RemoveChild($c)
            }
           }

        foreach ($c in $v.Content) {
            if ($c.HasAttribute("Include") -and $c.GetAttribute("Include") -like "*Jacobi.snk*") {
                $c.parentNode.RemoveChild($c)
            }
        }

        

    }

    $myXML.Save($project)
    
    
}


SetupVSTFiles

$projects = @("Jacobi.Vst.Core","Jacobi.Vst.Framework","Jacobi.Vst.Interop","Jacobi.Vst.UnitTest")
$clrs = @("Clr2", "Clr4")

foreach($project in $projects) {
  foreach ($clr in $clrs) {
  AdjustProjects $project $clr
  }

}

