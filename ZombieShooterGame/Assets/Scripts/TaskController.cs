
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
   
    static string[] tasks =
    {
        "Finish The Tutorial",
        "Beat The Nun Zombie",
        "Find The Prison Key",
        "Beat The Police Zombie",
        "Find The Cellar Key",
        "Beat The Boss"
    };
    public int taskOrderIndex = 0;
    KeyController keyController;
    [SerializeField] Text taskText;
    [SerializeField] Animator animator;
    private void Start()
    {
        keyController = FindObjectOfType<KeyController>();
        taskText.text = tasks[taskOrderIndex];
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
        taskText.text = tasks[taskOrderIndex];
    }
    
}
