using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class InteractWithBow : Interactable
{
    [SerializeField] GameObject glow, preview;

    void Awake()
    {
        onAttachedToHand += InteractWithBow_onAttachedToHand;
    }

    private void InteractWithBow_onAttachedToHand(Hand hand)
    {
        SpawnManager.INSTANCE.StartGame();
        glow.SetActive(false);
        preview.SetActive(false);
        Debug.Log("Start");
    }
}
