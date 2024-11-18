
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
   
    static string[] tasks =
    {
        "Finish The Tutorial",
        "Find The Key",
        "Destroy The Enemy",
        "Find The Key",
        "Beat The Boss"
    };

     KeyController keyController;
    [SerializeField] Text taskText;
    [SerializeField] Animator animator;
    private void Start()
    {
        keyController = FindObjectOfType<KeyController>();
        taskText.text = tasks[keyController.taskOrderIndex];
    }

    public void AssignTask()
    {
        StartCoroutine(DelayUpdate());
    }


    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("TaskUpdateTrigger");
        yield return new WaitForSeconds(3f);
        taskText.text = tasks[keyController.taskOrderIndex];
    }
    
}
