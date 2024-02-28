using System;
using System.Collections.Generic;
using BepInEx;
using UnityEngine;
using UnityEngine.Experimental.AI;
using Utilla;

namespace KultsBall
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
        private const double VX = -60.77;
		private float VXF = (float)VX;
		private const double VY = 3.892984;
		private float VYF = (float)VY;
		private const double VZ = -64.733;
		private float VZF = (float)VZ;

		private GameObject sphere;
		private GameObject speaker;
		private Rigidbody srb;
		private Material plasticmat;

        bool inRoom;

		void Start()
		{
			/* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
        }

		void Update()
		{
			/* Code here runs every frame when the mod is enabled */
		}

		/* This attribute tells Utilla to call this method when a modded room is joined */
		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "KultsBall";
            sphere.transform.SetParent(GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").transform);
            srb = sphere.AddComponent<Rigidbody>();
            srb.includeLayers = LayerMask.NameToLayer("Everything");
            speaker = GameObject.Find("Environment Objects/LocalObjects_Prefab/City/CosmeticsRoomAnchor/speaker");
            plasticmat = speaker.GetComponent<Renderer>().material;
            sphere.GetComponent<Renderer>().material = plasticmat;
            sphere.GetComponent<Renderer>().material.color = Color.grey;
            sphere.transform.position = new Vector3(VXF, VYF, VZF);
            inRoom = true;
		}

		/* This attribute tells Utilla to call this method when a modded room is left */
		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			/* Deactivate your mod here */
			/* This code will run regardless of if the mod is enabled*/
			Destroy(sphere);
			inRoom = false;
		}
	}
}
