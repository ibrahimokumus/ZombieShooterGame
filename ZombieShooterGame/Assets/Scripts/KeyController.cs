using System.Collections;
using UnityEngine;

public class KeyController : MonoBehaviour
{
   

    [SerializeField] GameObject[] keys; 
   

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
        if(doorController.doorOrderIndex > keys.Length) return;
        keys[doorController.doorOrderIndex].SetActive(false);
        SoundController.instance.PlayAddictinalSounds(1);
        if(doorController.doorOrderIndex ==2) doorController.OpenDoor(doorController.doorOrderIndex+1, -160f);
        else doorController.OpenDoor(doorController.doorOrderIndex, -160f);

        doorController.doorOrderIndex++;
        taskController.taskOrderIndex++;
       
    }

    public void MakeVisibleKey(int  keyIndex) 
    {
        if (keyIndex >= keys.Length) return;
        keys[keyIndex].SetActive(true);
    }

}
