using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBet : MonoBehaviour
{
    public Text total;
    public Text bet;

    // Start is called before the first frame update
    void Start()
    {
        total.GetComponent<Text>().text = "Total: " + Score.score;
        Score.bet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit, 40f))
            {
                if (hit.transform)
                {
                    if (hit.transform.gameObject.tag == "Chips1")
                    {
                       Score.bet += 1;
                        hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    }
                    else if (hit.transform.gameObject.tag == "Chips5")
                    {
                       Score.bet += 5;
                        hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    }
                    else if (hit.transform.gameObject.tag == "Chips10")
                    {
                       Score.bet += 10;
                        hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    }
                    else if (hit.transform.gameObject.tag == "Chips25")
                    {
                       Score.bet += 25;
                        hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    }
                    else if (hit.transform.gameObject.tag == "Chips50")
                    {
                       Score.bet += 50;
                        hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    }
                    
                }
            }

            if (Score.bet > Score.score)
            {
                Score.bet = Score.score;
            }

            Debug.Log(Score.bet);
        }

        bet.GetComponent<Text>().text = "Bet: " + Score.bet;

    }
}
