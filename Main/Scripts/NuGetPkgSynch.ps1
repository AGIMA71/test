###
### Use to script to update the Nuget public destination folder with packages from your local nuget cache.
### Author: Peter Puglisi (ucdd8)
### 
### Step 1: Modify the path in the first line with your user id.
###      - This script has only been tested against nuget packages in user cache folder but it should work on packages stored relative to the
###      - solution as well but you may need to modify both $localNugetParentFolder and $localNugetSubFolder accordingly.
###
### Step 2: For a new solution - recommended you clear out your local cache first and restore the nuget packages you need for 
### your starter solution to build locally - typically by pointing to https://api.nuget.org/v3/index.json.
###
### Step 3: Run the script .\NugetPkgSynch from the PowerShell Admin command window.
### The script will output what package folders were created (if any) and any nuget packages copied to the share.
###
### Step 4: Clear out your local cache - Tools->Options->NuGet Package Manager->General (Clear All NuGet Cache(s))
###
### Step 5: Go to Tools-> Options->NuGet Package Manager->Package Sources and *only* select the public nuget package folder. 
### Name:   Public 
### Source: \\ATOOBLD00002DEV.product.atotnet.gov.au\nuget\Public
###
### Step 6: Check in your changes and verify your check-in passes the gated checkin (which will pull packages from the nuget public folder)
###
###


$localNugetParentFolder = "C:\Users\ucdd8"  #Modify with your user id
$localNugetSubFolder = ".nuget\packages"

#$targetPublicFolder = 'C:\NUGET\public' # local test path
$targetPublicFolder = '\\ATOOBLD00002DEV.product.atotnet.gov.au\nuget\Public'

$sourceNugetPath = $localNugetParentFolder + "\" + $localNugetSubFolder

Write-Host ""
Write-Host "Synching local nuget packages with shared package sources folder... please wait..."

Get-ChildItem -Path $sourceNugetPath -Include "*.nupkg" -Recurse | ForEach-Object {

   $splitSource = $_.FullName -split '\\'
   $nugetPkgName = $splitSource[$splitSource.Length - 3]
   $nugetPkgFile = $splitSource[$splitSource.Length - 1]
   
   $destinationFolder = $targetPublicFolder + '\' + $nugetPkgName

   If (!(Test-Path $destinationFolder))
   {
       Write-Host "Creating new destination folder for " $destinationFolder
       mkdir $destinationFolder | Out-Null
   }
   
   if(![System.IO.File]::Exists($destinationFolder + '\' + $nugetPkgFile))
   {
       Write-Host "Copying nuget package" $nugetPkgFile "to" $destinationFolder
       Copy-Item $_.FullName -Destination $destinationFolder | Out-Null
   }
}

Write-Host ""
Write-Host "Nuget destination folder is now in synch with your local nuget packages."
