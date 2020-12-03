using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {
    public bool value;

	void Start () {
		this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        this.GetComponent<AudioSource>().Play();
		value = true;
	}
}