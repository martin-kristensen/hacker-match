var task = Argument("task", defaultValue: "Build");
var configuration = Argument("configuration", defaultValue: "Release");

Task("Clean")
    .Does(() =>
{
    // Clean directories.
    CleanDirectory("./build");
    CleanDirectories("./**/bin/" + configuration);
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    // Build project using MSBuild.
    MSBuild("./Tretton37.Knowabunga.sln", settings => 
        settings.SetPlatformTarget(PlatformTarget.x86)
            .UseToolVersion(MSBuildToolVersion.NET45)
            .WithProperty("Magic","1")
            .WithTarget("Build")
            .SetConfiguration(configuration));         
});

Task("Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    // Run unit tests.
    XUnit("./src/**/bin/" + configuration + "/*.Tests.dll");
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{   
    var root = "./Tretton37.Knowabunga.Web";
    var output = "./build/" + configuration + ".zip";
    var files = root + "/*";

    // Package the bin folder.
    Zip(root, output, files);
});

// Run the script.
Run(task);