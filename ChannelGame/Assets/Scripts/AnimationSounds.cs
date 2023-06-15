using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public AK.Wwise.Event EnrageSound;
    private bool hasEnraged = false;
    // Start is called before the first frame update
    void Anim_EnterEnrage()
    {
        if (!hasEnraged)
        {
            if (EnrageSound != null)
            {
                EnrageSound.Post(gameObject);
                //Debug.Log("SOUND HERE");
                hasEnraged = true;
            }
        }
    }
}
