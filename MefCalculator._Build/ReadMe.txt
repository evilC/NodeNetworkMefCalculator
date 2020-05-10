This project contains no code

It's sole purpose is to ensure that the plugins are built and ready before the main (Gui) project runs
All other projects should depend on this project
ie Right click root NodeNetworkMefCalculator solution -> Project Dependencies -> Dependencies tab.
Select each project from the Drop-down list and tick the MefCalculator._Build project checkbox

This ensures that this project gets built first and executes it's Pre-Build Event at the start of the build process
The Pre-Build Event for this project will wipe the Artifacts folder and then reconstruct it

MefCalculator.Gui SHOULD ALSO DEPEND UPON (SEE ABOVE) ALL PLUGINS 
This will ensure that the Artifacts folder is populated with all the built plugins before it runs
As it's Pre-Build Event, MefCalculator.Gui will copy all plugins from the Artifacts folder to it's Target folder

ie on compile, the sequence of events should be:

Build _Build
	Pre-Build: Remove Artifacts folder
	Pre-Build: Rebuild Artifacts folder structure

Build Editors
Build Node Plugins
	Post-Build: Each Node Plugin's Post-Build Event copies it's DLLs to the Artifacts folder

Build Gui
	Post-Build: Copy Plugins from Artifacts folder to Gui folder

Run App