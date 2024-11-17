using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageContoller : MonoBehaviour
{
    
    HealthBarController healthBarController;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBarController = FindObjectOfType<HealthBarController>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthBarController.Damage(15);
        }
    }
}
