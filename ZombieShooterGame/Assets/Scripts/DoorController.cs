using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DoorController : MonoBehaviour
{

    [SerializeField] GameObject[] doors;

    [SerializeField] Text doorOpenText;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] TaskController taskController;
    [SerializeField] NunController nunController;
    /// <summary>
    /// Anahtar bulununca, kapiyi acan method 
    /// </summary>
    /// <param name="index">Acilmasi gereken kapi indisi</param>
    /// <param name="angle">kapinin kac derece donecegi</param>
    public void OpenDoor(int index, float angle)
    {
        StartCoroutine(RotateDoorSmooth(index, angle));
        SoundController.instance.PlayAroundSounds(3);//kapi acilis sesi
        canvasAnimator.SetTrigger("DoorOpenTrigger");
        doorOpenText.text = "Somewhere, a door is opened ";
        taskController.AssignTask();
        if (index < 1)
        {
            nunController.canSeePlayer = true;
        }
       
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
