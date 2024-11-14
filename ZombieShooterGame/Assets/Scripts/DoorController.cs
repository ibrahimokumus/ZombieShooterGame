using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] GameObject[] doors;




    public void OpenDoor(int index, float angle)
    {
        StartCoroutine(RotateDoorSmooth(index, angle));
        SoundController.instance.PlayAroundSounds(3);//kapi acilis sesi
    }

    private IEnumerator RotateDoorSmooth(int index, float targetAngle)
    {
        float duration = 1.8f; // Donus suresi, degeri sesin uzunluguna gore verdim
        float elapsed = 0f;

        Transform doorTransform = doors[index].transform;
        Quaternion initialRotation = doorTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        while (elapsed < duration)
        {
            doorTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorTransform.rotation = targetRotation; //kayma varsa, tam o acida dursun diye
    }




}
