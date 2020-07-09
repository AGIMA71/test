## READ ME Notes for Developers

Note this is intentionally a markdownfile so it's easier to read and edit (but I have not checked it with a markdown viewer - can source a VS addin for this later.)

# Building solution for the first time

1. Run clean solution.

2. Clear out your local cache - Tools->Options->NuGet Package Manager->General (Clear All NuGet Cache(s))
... If you get an error due to some locking issue - just manually delete the nuget files from your local C:\Users\uxxxx\.nuget\packages

3. Make sure you are using the latest version of nuget.config file and make sure you are not pointing to https://api.nuget.org/v3/index.json.

4. Run "Restore NuGet packages on the solution".
... Note that this will restore all your nuget packages from the shared public folder and will take some times.
... Also note that additional nuget packages may get pulled down when you do a rebuild solution.

5. Run "Rebuild Solution".

5. Verify the solution builds for you successfully.


# Making changes to the solution and adding new nuget packages

1. This will be covered later when needed.
 