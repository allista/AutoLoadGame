# AutoLoadGame Change Log

## v1.0.10.1 / 2022-02-14

  * Do not copy AT_Utils dlls to output
  * KSP: MinMaxVersion: 1.11.1
  * KSP: reference changed 1.10.1 => 1.11.1
  * KSP refs: 1.10.0

## v1.0.10 / 2020-05-03

  * AssemblyVersion
  * Added AutoLoadGame to KSP-AVC-Updater
  * RF
  * Fixed debug error: ApplicationRootPath can only be called after constructor
  * Changed references to KSP-1.9.1

## v1.0.9 / 2019-11-14

  * AF: csproj
  * Don't generate pdb in Release configuration
  * AssemblyVersion: 1.0.9
  * Set new UnityEngine.Module dlls as non-private: don't copy them to the output
  * Added required Unity-2019 Module dlls
  * Changed target framework to .NET-4.5
  * REFS KSP-1.8.1
  * REFS KSP 1.7.3
  * Added NIGHTBUILD def to the corresponding configuration
  * REFS: KSP-1.7.0
  * REFS: KSP-1.6.0

## v1.0.8 / 2018-08-31

  * Changed version to 1.0.8
  * Made ALG into a singleton to fix an NRE.
  * Added .pdb to ignore
  * Changed references to KSP-1.4.5
  * Removed redundant explicit reference to AutoLoadGame class.
  * Changed references to KSP-1.4.3
  * Corrected .version file

## v1.0.7 / 2018-03-28

  * Changed version to 1.0.7
  * Added *.pdb to excludes
  * Added 60frame delay before leaving the MAINMENU scene.
  * Changed references to KSP-1.4.1

## v1.0.6 / 2016-12-25

  * Changed version to 1.0.6
  * No need to resave the just-loaded game.
  * Adapted to KSP-1.2.2 changes.

## v1.0.5.1 / 2016-12-20

  * Recompiled against 1.2.2; changed version to 1.0.5.1
  * Changed references to 1.2.2
  * Moved AutoLoadGame.version under mod's folder where it should be.
  * Updated version file. The mod itself is compatible without change.

## v1.0.5 / 2016-11-03

  * Changed references to 1.2.1; changed version to 1.0.5

## v1.0.4 / 2016-10-27

  * Changed version to 1.0.4. Moved .version file into the GameData.
  * Fixed some NREs in other mods caused by trying to load the game too early.
  * Added AutoLoadGame.version
  * Create AutoLoadGame_KSP-AVC

## v1.0.3 / 2016-10-15

  * Added MonoDevelop .userprefs to ignore.
  * Updated to KSP-1.2 API. Added separate solution to compile just this project.

## v1.0.2 / 2016-09-30

  * Changed version to 1.0.2
  * Removed reference to AT_Utils. Closed #3
  * Added make-release.sh script that uses make_mod_release from PyKSPUtils.
  * Added Releases and obj to ignore.

## v1.0.1 / 2016-08-02

  * Renamed AutoLoadGame.conf to AutoLoadGame_example.conf
  * Changed version to 1.0.1
  * Fixed #2. The game is now loaded only once, just after game start.
  * Added SpaceDock top image.
  * Added default config file.

## v1.0 / 2016-07-09

  * Updated README.md
  * Initial commit. Working.
