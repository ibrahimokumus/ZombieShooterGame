using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    PlayerController player;
    [SerializeField] Text countDownTxt;
    [SerializeField] GameObject startPanel;
    int countDownTime = 5;
    // panel veya IU manager cagir


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(CountDown());
    }



  



    IEnumerator CountDown()
    {
        int currentTime = countDownTime;
        yield return new WaitForSeconds(1);

        while (currentTime > 0)
        {
            countDownTxt.text = "The game will be started  after " + currentTime.ToString() + " seconds" ;
            yield return new WaitForSeconds(1);
            currentTime--;
        }

        countDownTxt.text = "Let's GO!";
        yield return new WaitForSeconds(2);
        startPanel.SetActive(false);
        Cursor.visible = false; // Mouse görünmez yap
        Cursor.lockState = CursorLockMode.Locked;
        player.canMove = true;
        Debug.Log(player.canMove);
    }
   
    


}
