using System.Collections;
using UnityEngine;

public class KeyController : MonoBehaviour
{
   

    [SerializeField] GameObject[] keys; 
    public int taskOrderIndex = 0;

    TaskController taskController;
    DoorController doorController;
    EnemyBaseClass enemyBaseClass;
    private void Start()
    {
        taskController = FindObjectOfType<TaskController>();
        doorController = FindObjectOfType<DoorController>();
        
        
       
        foreach (var key in keys)
        {
            key.SetActive(false);// basta kapatiyoz hepsini, gorevler ilerldikce acacagiz
        }
        keys[0].SetActive(true);
        
    }


    public void KeyPickUpKey()
    {  
        keys[taskOrderIndex].SetActive(false);
        SoundController.instance.PlayAddictinalSounds(1);
        doorController.OpenDoor(taskOrderIndex,-160f);
        taskOrderIndex++;
       
    }

    public void MakeVisibleKey(int  keyIndex) 
    {
        if (keyIndex >= keys.Length) return;
        keys[keyIndex].SetActive(true);
    }

}
