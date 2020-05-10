Procedure for adding a new plugin:

(See ReadMe.txt in _Build project for what is meant by "Depends Upon")

1) Make the plugin project depends upon the _Build project
2) Make sure that the MefCalculator.Gui project depends on the plugin
3) Add a Post-Build event to copy the plugin binaries to the Artifacts\Nodes folder in the root folder of the solution
	You can most likely just copy the Post-Build event verbatim from an existing plugin
4) Ensure that the plugin DLL name is the same as the name as the folder it is in
ie if the plugin name is "MefCalculator.Plugins.Nodes.Foo", it should end up as Artifacts\Nodes\Plugins\MyPlugin\MyPlugin.dll
