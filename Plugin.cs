﻿using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using Wetstone.API;

namespace LeadAHorseToWater
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInDependency("xyz.molenzwiebel.wetstone")]
	[Wetstone.API.Reloadable]
	public class Plugin : BasePlugin
	{
		public static ManualLogSource LogInstance { get; private set; }

		private HarmonyLib.Harmony _harmony;
		public override void Load()
		{
			LogInstance = this.Log;
			Settings.Initialize(Config);
			
			// Server plugin check
			if (!VWorld.IsServer)
			{
				Log.LogWarning("This plugin is a server-only plugin.");
				return;
			}

			// Plugin startup logic
			_harmony = new HarmonyLib.Harmony(PluginInfo.PLUGIN_GUID);
			_harmony.PatchAll();
			Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
		}

		public override bool Unload()
		{
			_harmony.UnpatchSelf();
			return true;
		}
	}
}
