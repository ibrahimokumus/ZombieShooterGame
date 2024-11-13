using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBullet : BulletBaseClass
{
   

    private void Start()
    {
        fireController = FindObjectOfType<FireController>();
        gameManager = FindObjectOfType<GameManager>();
        bulletAmount = 10;
    }



    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            //fireController.PickUpAmmoTrigger(bulletAmount);
           // gameManager.BulletsDeactivate(0);
           
        }
    }

}
