using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Text countDownTxt;
    [SerializeField] GameObject startPanel;
    [SerializeField] int countDownTime = 5;
    [SerializeField] Transform[] spawnPoints;
    PlayerController player;
    DoorController  doorController;
    static int deathCount = 0;
    private void Start()
    {
        player = playerPrefab.GetComponent<PlayerController>();
        doorController = FindObjectOfType<DoorController>();
        StartCoroutine(CountDown());
    }


    private void Update()
    {
        if (player.isDied)
        {
            deathCount++;
            // Instantiate(playerPrefab, spawnPoints[doorController.doorOrderIndex].position,Quaternion.identity);
            startPanel.SetActive(true);
            countDownTxt.text = "YOU DIED";
            Invoke("LoadDeathScene", 3f);
        }
    }




    IEnumerator CountDown()
    {
        if (deathCount<1)
        {

            int currentTime = countDownTime;
            yield return new WaitForSeconds(1);

            while (currentTime > 0)
            {
                countDownTxt.text = "The game will be started  after " + currentTime.ToString() + " seconds";
                yield return new WaitForSeconds(1);
                currentTime--;
            }

            countDownTxt.text = "Let's GO!";
            yield return new WaitForSeconds(2);
            startPanel.SetActive(false);
            Cursor.visible = false; // Mouse görünmez yap
            Cursor.lockState = CursorLockMode.Locked;
            player.canMove = true;
        }
        else
        {
            int currentTime = countDownTime;
            yield return new WaitForSeconds(1);

            while (currentTime > 0)
            {
                countDownTxt.text = "You will be born  after " + currentTime.ToString() + " seconds";
                yield return new WaitForSeconds(1);
                currentTime--;
            }

            countDownTxt.text = "Come on, do it!";
            yield return new WaitForSeconds(2);
            startPanel.SetActive(false);
            Cursor.visible = false; // Mouse görünmez yap
            Cursor.lockState = CursorLockMode.Locked;
            player.canMove = true;
        }

    }



    void LoadDeathScene()
    {
        SceneManager.LoadScene("Map");
    }




}
