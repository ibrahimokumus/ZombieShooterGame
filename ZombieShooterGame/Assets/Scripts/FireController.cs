using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
   
   
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
        RaycastHit hit;
       

        Vector3 rayOrigin = Camera.main.transform.position; // kamera konumu
        Vector3 rayDirection = Camera.main.transform.forward; // kamera yonu
       // Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red, 20f);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, distance))
        {
            Debug.Log("Isabet eden nesne: " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                // Damage methodunu cagir
            }
        }

    }

    public void PickUpAmmoTrigger(int amount)
    {
        StartCoroutine(PickUpAmmo(amount));
    }
    IEnumerator PickUpAmmo(int amount)
    {
        // mermi toplama sesi cal
        for (int i = 0; i < amount; i++)
        {
            totalBullet += 1;
            bulletText.text = bulletCount.ToString() + " / " + totalBullet.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
