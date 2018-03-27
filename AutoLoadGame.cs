//  Author:
//       Allis Tauri <allista@gmail.com>
//
//  Copyright (c) 2016 Allis Tauri

using System;
using System.IO;
using UnityEngine;

using SaveUpgradePipeline;

namespace AutoLoadGame
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class AutoLoadGame : MonoBehaviour
    {
        static bool Loaded;
        static readonly string savesdir = Path.Combine(KSPUtil.ApplicationRootPath, "saves");
        static readonly string config   = Path.Combine(savesdir, "AutoLoadGame.conf");

        public static void Log(string msg, params object[] args) 
        { Debug.Log(string.Format("[AutoLoadGame]: "+msg, args)); }

        string game = "";
        string save = "";

        void LoadGame()
        {
            //load the game only the first time (i.e. at game start)
            if(Loaded) return;
            Loaded = true;
            //get the game and the save
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
                    Log("Configuration file is empty: {0}", config);
                    return;
                }
            }
            else 
            {
                Log("Configuration file not found: {0}", config);
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
            var game_node = GamePersistence.LoadSFSFile(save, game);
            if(game_node == null)
            {
                Log("Unable to load the save: {0}", savefile);
                return;
            }
            Log("Loading: {0}/{1}", game, save);
            KSPUpgradePipeline.Process(game_node, game, LoadContext.SFS, OnLoadDialogPipelineFinished, 
                                       (opt, n) => Log("KSPUpgradePipeline finished with error: {0}", savefile));
        }

        void OnLoadDialogPipelineFinished(ConfigNode node)
        {
            HighLogic.CurrentGame = GamePersistence.LoadGameCfg(node, game, true, false);
            if(HighLogic.CurrentGame != null)
            {
                if(GamePersistence.UpdateScenarioModules(HighLogic.CurrentGame))
                {
                    if(node != null) GameEvents.onGameStatePostLoad.Fire(node);
                    GamePersistence.SaveGame(HighLogic.CurrentGame, save, game, SaveMode.OVERWRITE);
                }
                if(HighLogic.CurrentGame.startScene == GameScenes.FLIGHT)
                {
                    AutoSwitchVessel.activeVessel = HighLogic.CurrentGame.flightState.activeVesselIdx;
                    AutoSwitchVessel.save = save;
                }
                HighLogic.CurrentGame.startScene = GameScenes.SPACECENTER;
                HighLogic.SaveFolder = game;
                HighLogic.CurrentGame.Start();
            }
        }

        void onLevelWasLoaded(GameScenes scene) 
        { 
            if(scene == GameScenes.MAINMENU) 
            {
                AutoLoadGame.Log("MAINMENU is loaded. Waiting 60 frames and loading the save.");
                StartCoroutine(CallbackUtil.DelayedCallback(60, LoadGame));
            }
        }

        void Awake()
        { 
            GameEvents.onLevelWasLoadedGUIReady.Add(onLevelWasLoaded); 
        }
    }

    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class AutoSwitchVessel : MonoBehaviour
    {
        public static string save = "persistent";
        public static int activeVessel = -1;

        static void switch_to_active_vessel()
        {
            FlightDriver.StartAndFocusVessel(save, activeVessel);
            activeVessel = -1;
        }

        void onLevelWasLoaded(GameScenes scene)
        { 
            if(scene == GameScenes.SPACECENTER && activeVessel >= 0)
            {
                AutoLoadGame.Log("SPACECENTER is loaded. Waiting 60 frames and switching to the active vessel.");
                StartCoroutine(CallbackUtil.DelayedCallback(60, switch_to_active_vessel));
            }
        }

        void Awake()
        { 
            GameEvents.onLevelWasLoadedGUIReady.Add(onLevelWasLoaded); 
        }
    }
}

