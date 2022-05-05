using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpDot : MonoBehaviour
{
    private void OnEnable()
    {
        int x = Random.Range(1, 16);
        if (x < 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0,1,0);
            // fuel add power up
        }
        else if (x < 6)
        {
            GetComponent<SpriteRenderer>().color = new Color(1,0,0);

            // stop power-up
        }
    }
}