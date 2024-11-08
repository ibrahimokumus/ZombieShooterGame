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

    public int bulletAmount = 10;
    public Text bulletText;

    private void Start()
    {
        bulletText.text = bulletAmount.ToString();
    }
    public void Fire()
    {

        SoundController.instance.PlaySoundEffect(0);
        bulletAmount -= 1;
        bulletText.text = bulletAmount.ToString();
        if (Physics.Raycast(spawnPos.transform.position, spawnPos.transform.forward, out hit, distance))
        {
          
            Debug.Log("Isabet eden nesne: " + hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                //Damage methodunu cagir

            }
        }
    }
}
