# AutoLoadGame

Simple KSP plugin that automatically loads the specified savegame upon game start.

To use it, create the **`AutoLoadGame.conf`** file in your **`saves`** directory and put the following two options there:

        game = YouGameFolder
        save = savegame
    
The savegame name may be a filename of any *file.sfs* under *YouGameFolder*, but it should be given WITHOUT the extension. For example, use:

        game = default
        save = persistent
        
to automatically load your default game in the state you'v left it.   
