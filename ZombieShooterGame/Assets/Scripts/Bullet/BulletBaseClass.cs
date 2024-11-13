using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBaseClass : MonoBehaviour
{
    
    public int bulletAmount;
    protected GameManager gameManager;
   [SerializeField] protected FireController fireController;

    

    protected abstract void OnTriggerEnter(Collider other);

   
}
