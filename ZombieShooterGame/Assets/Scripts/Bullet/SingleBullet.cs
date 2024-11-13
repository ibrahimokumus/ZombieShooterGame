using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBullet : BulletBaseClass
{

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fireController = FindObjectOfType<FireController>();
        bulletAmount = 5;
    }



    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           // fireController.PickUpAmmoTrigger(bulletAmount);
           // gameManager.BulletsDeactivate(1);
        }
    }
}
