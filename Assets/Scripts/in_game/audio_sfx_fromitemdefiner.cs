using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_sfx_fromitemdefiner : MonoBehaviour
{
    public void TryAttackSound(ItemDefiner toTry)
    {
        AudioManager.Instance.parseInteractSoundFrom(toTry);
    }
}
