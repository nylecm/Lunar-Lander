using UnityEngine;

public abstract class PowerUp
{
    protected AudioClip AcquiredSound;
    //protected Sprite Sprite;

    protected PowerUp(AudioClip acquiredSound)
    {
        AcquiredSound = acquiredSound;
    }
    
    public abstract void Activate();

    protected void PlaySound()
    {
        
    }
}