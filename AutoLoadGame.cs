//   MyClass.cs
//
//  Author:
//       Allis Tauri <allista@gmail.com>
//
//  Copyright (c) 2016 Allis Tauri

using System;
using System.IO;
using UnityEngine;
using AT_Utils;

namespace AutoLoadGame
{
	[KSPAddon(KSPAddon.Startup.MainMenu, false)]
	public class AutoLoadGame : MonoBehaviour
	{
		static readonly string savesdir = KSPUtil.ApplicationRootPath+"saves";
		static readonly string config   = Path.Combine(savesdir, "AutoLoadGame.conf");

		void Awake()
		{
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
					Utils.LogF("LoadTestGame: Configuration file is empty: {}", config);
					return;
				}
			}
			else 
			{
				Utils.LogF("LoadTestGame: Configuration file not found: {}", config);
				return;
			}
			var savefile = Utils.PathChain(savesdir, game, save+".sfs");
			if(!File.Exists(savefile)) 
			{
				Utils.LogF("No such file: {}", savefile);
				return;
			}
			HighLogic.CurrentGame = GamePersistence.LoadGame(save, game, false, false);
			if (HighLogic.CurrentGame != null)
			{
				GamePersistence.UpdateScenarioModules(HighLogic.CurrentGame);
				HighLogic.SaveFolder = game;
				HighLogic.CurrentGame.Start();
			}
		}
	}
}

