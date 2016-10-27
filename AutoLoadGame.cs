//  Author:
//       Allis Tauri <allista@gmail.com>
//
//  Copyright (c) 2016 Allis Tauri

using System;
using System.IO;
using UnityEngine;

namespace AutoLoadGame
{
	[KSPAddon(KSPAddon.Startup.MainMenu, false)]
	public class AutoLoadGame : MonoBehaviour
	{
		static bool Loaded;
		static readonly string savesdir = Path.Combine(KSPUtil.ApplicationRootPath, "saves");
		static readonly string config   = Path.Combine(savesdir, "AutoLoadGame.conf");

		static void Log(string msg, params object[] args) { Debug.Log(string.Format(msg, args)); }

		void LoadGame()
		{
			//load the game only the first time (i.e. at game start)
			if(Loaded) return;
			Loaded = true;
			//get the game and the save
			var game = "";
			var save = "";
			if(File.Exists(config))
			{
				var cfg = ConfigNode.Load(config);
				if(cfg != null)
				{
					var val = cfg.GetValue("game");
					if(val != null) game = val;
					val = cfg.GetValue("save");
					if(val != null) save = val;
				}
				else 
				{
					Log("LoadTestGame: Configuration file is empty: {0}", config);
					return;
				}
			}
			else 
			{
				Log("LoadTestGame: Configuration file not found: {0}", config);
				return;
			}
			var gamedir = Path.Combine(savesdir, game);
			if(!Directory.Exists(gamedir))
			{
				Log("No game directory: {0}", gamedir);
				return;
			}
			var savefile = Path.Combine(gamedir, save+".sfs");
			if(!File.Exists(savefile)) 
			{
				Log("No such file: {0}", savefile);
				return;
			}
			//load the game
			HighLogic.CurrentGame = GamePersistence.LoadGame(save, game, false, false);
			if (HighLogic.CurrentGame != null)
			{
				GamePersistence.UpdateScenarioModules(HighLogic.CurrentGame);
				HighLogic.SaveFolder = game;
				HighLogic.CurrentGame.Start();
			}
		}

		void onLevelWasLoaded(GameScenes scene)
		{ if(scene == GameScenes.MAINMENU) LoadGame(); }

		void Awake()
		{ GameEvents.onLevelWasLoaded.Add(onLevelWasLoaded); }
	}
}

