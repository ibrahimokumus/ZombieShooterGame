using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{

    [SerializeField] ParticleSystem[] particleEffects;
   

    public void PlayParticleEffect(int index)
    {
        if (GetComponent<ParticleSystem>() == null) return;
        particleEffects[index].Stop();
        particleEffects[index].Play();
    }
}
