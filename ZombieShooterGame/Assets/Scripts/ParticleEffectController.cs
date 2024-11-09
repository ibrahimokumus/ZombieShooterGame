using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ParticleEffect
{
    public string effectName;
    public ParticleSystem[] particleSystems;
}

public class ParticleEffectController : MonoBehaviour
{
    public ParticleEffect[] particleEffectsArray;

    private Dictionary<string, ParticleSystem[]> particleEffects = new Dictionary<string, ParticleSystem[]>();

    private void Start()
    {
        // particleEffectsArray içindeki her bir efekti dictionary'ye ekle
        foreach (ParticleEffect effect in particleEffectsArray)
        {
            particleEffects[effect.effectName] = effect.particleSystems;
        }
    }

    public void PlayParticleEffect(string effectName)
    {
        if (!particleEffects.ContainsKey(effectName)) return;

        foreach (ParticleSystem particles in particleEffects[effectName])
        {
            particles.Stop();
            particles.Play();
        }
    }
}