using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupShake : MonoBehaviour
{
    GameObject Cover;
    public float shakeSpeed;
    public float shakeAmount;
    public float rotateSpeed;
    public float raiseSpeed;
    bool shakeCup;
    bool rotateCup;
    bool raiseCup;
    bool audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        shakeCup = false;
        rotateCup = false;
        raiseCup = false;
        Cover = this.gameObject.transform.GetChild(0).gameObject;
        Cover.SetActive(false);
        StartCoroutine(ShakeCupCoroutine());
        
    }

    public void Reposition()
    {
        shakeCup = false;
        rotateCup = false;
        raiseCup = false;
        audioPlay = false;
        Cover = this.gameObject.transform.GetChild(0).gameObject;
        Cover.SetActive(false);
        this.transform.position = new Vector3(0.7f, 8.18f, 2.11f);
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        StartCoroutine(ShakeCupCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeCup)
        {
            if (!audioPlay)
            {
                this.GetComponent<AudioSource>().Play();
                audioPlay = true;
            }
            
            transform.Translate(Vector3.up * Mathf.Sin(Time.time * shakeSpeed) * shakeAmount * Time.deltaTime);
        }
        if (rotateCup)
        {
            transform.Rotate(rotateSpeed, 0.0f, 0.0f, Space.Self);

            if (transform.eulerAngles.x > 180)
            {
                rotateCup = false;
                raiseCup = true;
            }
        }
        if (raiseCup)
        {
            transform.Translate(Vector3.down * raiseSpeed * Time.deltaTime);
            if(transform.position.y > 35)
            {
                raiseCup = false;
            }
        }
    }

    IEnumerator ShakeCupCoroutine()
    {
        yield return new WaitForSeconds(2);
        Cover.SetActive(true);
        shakeCup = true;
        yield return new WaitForSeconds(2);
        shakeCup = false;
        Cover.GetComponent<Collider>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        Cover.SetActive(false);
        rotateCup = true;
    }
}
