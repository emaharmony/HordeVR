using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportingActions : MonoBehaviour
{
    [Range(10,50)] public int staminaCount = 10;
    [Range(1, 10)] public int secondsToRecharge = 5;
    public UnityEvent staminaEmpty;
    public UnityEvent staminaRecharge;

    private int currentStamina; 
    private float recharge = 0;

    private void Start()
    {
        currentStamina = staminaCount;
    }

    public void DecreaseStamina()
    {
        if (currentStamina > 0)
        {
            CancelInvoke("Recharge");
            currentStamina--;
        }
        else
        {
            staminaEmpty.Invoke();
        }

        Invoke("Recharge", recharge);
    }

    void Recharge()
    {
        currentStamina = staminaCount;
        AudioPlayer.Instance.Ding();
        staminaRecharge.Invoke();
    }
}
