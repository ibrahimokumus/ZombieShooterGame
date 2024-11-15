
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

    private void Start()
    {
        keyController = FindObjectOfType<KeyController>();
        taskText.text = tasks[keyController.taskOrderIndex];
    }

    public void AssignTask()
    {
        Invoke("DelayUpdate", 3f);
    }

    void DelayUpdate ()
    {
        taskText.text = tasks[keyController.taskOrderIndex];
    }
}
