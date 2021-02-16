using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MagicManager : MonoBehaviour 
{
	[Range(1,3)]
	[SerializeField] int maxManaCharge = 3;
	[SerializeField] Spell magicPrefab;
    [SerializeField] float spellCoolDown = 3.0f;

    Hand controller;
    int currManaCharge;
    bool _ready = true;
    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources _Sources = SteamVR_Input_Sources.LeftHand;

    public bool devMode = true, shootSpell = false;

    private void Awake()
    {
        grabPinch.AddOnStateUpListener(OnTriggerPressedOrReleased, _Sources);
    }

    private void OnDisable()
    {
        grabPinch.RemoveOnStateUpListener(OnTriggerPressedOrReleased, _Sources);
    }
    void Start() 
	{
        controller = GetComponentInParent<Hand>();
		currManaCharge = maxManaCharge;
        string[] s = Input.GetJoystickNames();
        foreach(string x in s)
            Debug.Log(x);

    }

	void FixedUpdate()
	{

        if ((Input.GetButtonUp("FireMagic") || shootSpell) && _ready)
        {
            CastSpell();
        }

        else if (!_ready && shootSpell)
        {
            shootSpell = false;
        }
	}

    /// <summary>
    /// Shoot a spell and then wait for cool down time. 
    /// </summary>
	void CastSpell()
	{
		if ((currManaCharge != 0) && _ready)
		{
            _ready = false;
            Instantiate(magicPrefab, transform.position, transform.rotation);
			currManaCharge--;
            Invoke("ReadySpell", spellCoolDown);
		}
	}

    private void OnTriggerPressedOrReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        CastSpell();
    }
    /// <summary>
    /// Play the magic ready audio and set the ready bool to true so player can fire again.
    /// </summary>
    void ReadySpell()
    {
        _ready = true;
    }
}
