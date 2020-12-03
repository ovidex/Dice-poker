using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxBet : MonoBehaviour
{
    void Start () {
		this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		this.GetComponent<AudioSource>().Play();

        Score.bet = Score.score;
	}
}
