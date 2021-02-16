using Horde;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMode : MonoBehaviour
{

	public static DebugMode Instance { get; private set;}

	[SerializeField]GameObject debugCanvas;

	[SerializeField]HordeBow crossBow;
	[SerializeField]Light directionalLight;
	[SerializeField]SpawnPoint spawns;

	Button spawn, ammo, lights;
	Text shots;

	public string Victim{ get; set;}

	void Awake()
	{
		#if UNITY_EDITOR
		Instance = this;
		Instantiate(debugCanvas);
		#endif
	}

	// Use this for initialization
	void Start ()
    {
		#if UNITY_EDITOR
		spawn = GameObject.Find("SpawnDebug").GetComponent<Button>();
		ammo= GameObject.Find("AmmoDebug").GetComponent<Button>();
		lights = GameObject.Find("LightDebug").GetComponent<Button>();
		shots = GameObject.Find("ShotDebug").GetComponent<Text>();

		spawn.onClick.AddListener(SpawnDebugToggle);
		ammo.onClick.AddListener(AmmoDebugToggle);
		lights.onClick.AddListener(LightDebugToggle);
	
		SPAWN_DEBUG = true;
		LIGHT_DEBUG = false;
		AMMO_DEBUG = false;

		directionalLight.enabled = LIGHT_DEBUG;

		UpdateDebugUI();
		#endif
    }

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F1))
			SpawnDebugToggle ();
		if (Input.GetKeyDown (KeyCode.F2))
			AmmoDebugToggle ();
		if (Input.GetKeyDown (KeyCode.F3))
			LightDebugToggle ();
	}	

	void SpawnDebugToggle()
	{
		SPAWN_DEBUG = !SPAWN_DEBUG;
		UpdateDebugUI ();
	}

	void AmmoDebugToggle()
	{
		AMMO_DEBUG = !AMMO_DEBUG;
		UpdateDebugUI ();
	}

	void LightDebugToggle()
	{
		LIGHT_DEBUG = !LIGHT_DEBUG;
		directionalLight.enabled = LIGHT_DEBUG;
		UpdateDebugUI ();
	}

	void ShotsDebugUpdate()
	{
		//next
	}

	void UpdateDebugUI()
	{
		spawn.GetComponentInChildren<Text> ().text = "SPAWN: " + (SPAWN_DEBUG ? "ON" : "OFF");
		ammo.GetComponentInChildren<Text> ().text = "AMMO: " + (AMMO_DEBUG ? "ON" : "OFF");
		lights.GetComponentInChildren<Text> ().text = "LIGHT: " + (LIGHT_DEBUG ? "ON" : "OFF");
	}

	public bool SPAWN_DEBUG { get; set;}
	public bool LIGHT_DEBUG { get; set;}
	public bool AMMO_DEBUG { get; set;}
}
