using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowDices : MonoBehaviour
{
    public Camera mainCamera;
    public Camera zoomCamera;
    public Camera panelCamera;
    public Button replayButton;
    public Text total;
    public Text bet;
    public Text win;
    public float closureSpeed;
    public bool audioPlay;
    bool closeCamera;

    [HideInInspector]
    public int begin;
    GameObject[] Dices;

    // Start is called before the first frame update
    void Start()
    {
        Dices = new GameObject[5];
        mainCamera.enabled = true;
        zoomCamera.enabled = false;
        panelCamera.enabled = false;
        closeCamera = false;

        replayButton.enabled = false;
        replayButton.gameObject.SetActive(false);
        total.gameObject.SetActive(false);
        bet.gameObject.SetActive(false);
        win.gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            Dices[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (begin >= 5)
        {
            mainCamera.enabled = false;
            zoomCamera.enabled = true;
            replayButton.enabled = true;
            replayButton.gameObject.SetActive(true);
            total.gameObject.SetActive(true);
            bet.gameObject.SetActive(true);
            win.gameObject.SetActive(true);

            total.GetComponent<Text>().text = "Total: " + Score.score;
            bet.GetComponent<Text>().text = "Bet: " + Score.bet;

            for (int i = 0; i < 5; i++)
            {
                Dices[i].transform.eulerAngles = new Vector3(Dices[i].transform.eulerAngles.x, 0, Dices[i].transform.eulerAngles.z);
                float xpos = i - 2;
                Dices[i].transform.position = new Vector3(xpos, -1.25f, 16f);
            }
            closeCamera = true;
            if (closeCamera)
            {
                zoomCamera.transform.Translate(Vector3.forward * closureSpeed * Time.deltaTime);
                
                if(zoomCamera.transform.position.y < 2)
                {
                    closeCamera = false;
                }
            }

            begin = 4;
        }

        if (replayButton.GetComponent<ButtonPress>().value == true)
        {
           Score.score += getCurrentWin(); 

            replayButton.GetComponent<ButtonPress>().value = false;
            replayButton.enabled = false;
            replayButton.gameObject.SetActive(false);
            total.gameObject.SetActive(false);
            bet.gameObject.SetActive(false);
            win.gameObject.SetActive(false);
            Debug.Log(Score.score);

            SceneManager.LoadScene("Bet", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown("f1") && zoomCamera.enabled == true)
        {
            Debug.Log("Here");
            zoomCamera.enabled = false;
            panelCamera.enabled = true;
        }
        
        if (Input.GetKeyUp("f1") && panelCamera.enabled == true)
        {
            zoomCamera.enabled = true;
            panelCamera.enabled = false;
        }

        win.GetComponent<Text>().text = "Win: " + getCurrentWin();
    }

    int getCurrentWin()
    {
        int exit = this.GetComponent<Logic>().exit;
        int winSum = 0;

        switch (exit)
        {
            case 0:
                winSum = Mathf.FloorToInt(100 * Score.bet);
                break;
            case 1:
                winSum = Mathf.FloorToInt(50 * Score.bet);
                break;
            case 2:
                winSum = Mathf.FloorToInt(20 * Score.bet);
                break;
            case 3:
                winSum = Mathf.FloorToInt(15 * Score.bet);
                break;
            case 4:
                winSum = Mathf.FloorToInt(10 * Score.bet);
                break;
            case 5:
                winSum = Mathf.FloorToInt(2 * Score.bet);
                break;
            case 6:
                winSum = Mathf.FloorToInt(1.25f * Score.bet);
                break;
            case 7:
                winSum = Mathf.FloorToInt(0.5f * Score.bet);
                break;
            case 8:
                winSum = Mathf.FloorToInt(0 * Score.bet);
                break;
            default:
                break;
        }

        return winSum;
    }
}
