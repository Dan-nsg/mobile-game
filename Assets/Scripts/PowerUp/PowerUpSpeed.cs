using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUpBase
{
    [Header("Power Up -> SPEED")]
    public float amountToSpeed;
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpSpeed(amountToSpeed);
        PlayerController.Instance.SetPowerUpText("Speed Up");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetSpeed();
        PlayerController.Instance.SetPowerUpText(" ");
    }
}
