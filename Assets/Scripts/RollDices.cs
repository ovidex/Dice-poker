using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RollDices : MonoBehaviour
{
    void Start () {
		this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		this.GetComponent<AudioSource>().Play();

        if (Score.bet > 0)
        {
            Score.score -= Score.bet;
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
	}
}
