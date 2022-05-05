using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpDot : MonoBehaviour
{
    public PowerUpType PowerUpType;
    
    private void OnEnable()
    {
        int x = Random.Range(1, 16);
        if (x < 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0,1,0);
            PowerUpType = PowerUpType.FUEL;
            // fuel add power up
        }
        else if (x < 6)
        {
            GetComponent<SpriteRenderer>().color = new Color(1,0,0);
            PowerUpType = PowerUpType.STOP;
            // stop power-up
        }
        else
        {
            enabled = false;
        }
    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().color = new Color(0,0,0);
    }
}

public enum PowerUpType
{
    FUEL,
    STOP
}