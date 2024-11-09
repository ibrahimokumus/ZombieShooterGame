using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    RaycastHit hit;
    public Transform spawnPos;
    public bool canFire =true;
    public float gunTimer;
    public float autoFireRate;
   
    public float distance;

    public int bulletCount = 10;
    public int magazinAmount = 10;
    public Text bulletText;
    public int totalBullet = 100;
    ParticleEffectController particleEffectController;
    private void Start()
    {
        bulletText.text = bulletCount.ToString() + " / " + totalBullet.ToString();
        particleEffectController = FindObjectOfType<ParticleEffectController>();
    }
    public void Fire()
    {

        SoundController.instance.PlaySoundEffect(0);
        particleEffectController.PlayParticleEffect("Muzzle");

        bulletCount -= 1;
        bulletText.text = bulletCount.ToString() +" / "+ totalBullet.ToString();

        if (Physics.Raycast(spawnPos.transform.position, spawnPos.transform.forward, out hit, distance))
        {
          
            Debug.Log("Isabet eden nesne: " + hit.transform.name);
          /*  if (hit.transform.CompareTag("Enemy"))
            {
                //Damage methodunu cagir

            }*/
        }
    }
}
