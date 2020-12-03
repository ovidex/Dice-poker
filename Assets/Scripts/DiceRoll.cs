using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 startPosition;
    public int minSpeed;
    public int maxSpeed;
    public int upFace;
    float threshold;
    float rotationError;
    Vector3 rotateSpeed;
    bool inAir;
    bool onTable;
    bool resultDisplayed;
    public Camera zoomCamera;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.transform.position = startPosition;
        this.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        rotateSpeed = new Vector3(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
        inAir = true;
        onTable = false;
        resultDisplayed = false;
        threshold = 0.1f;
        rotationError = 45;
    }

    public void Reposition()
    {
        rb = GetComponent<Rigidbody>();
        this.transform.position = startPosition;
        this.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        rotateSpeed = new Vector3(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
        transform.parent.GetComponent<ShowDices>().audioPlay = false;
        inAir = true;
        onTable = false;
        resultDisplayed = false;
        threshold = 0.1f;
        rotationError = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (inAir)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime);
        }

        if (onTable && !resultDisplayed)
        {
            if (rb.velocity.x < threshold && rb.velocity.y < threshold && rb.velocity.z < threshold)
            {
                resultDisplayed = true;
                StartCoroutine(DiceCheckCoroutine());
            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cup") || collision.gameObject.CompareTag("Table"))
        {
            inAir = false;
        }

        if (collision.gameObject.CompareTag("Table"))
        {
            onTable = true;

            if (!transform.parent.GetComponent<ShowDices>().audioPlay)
            {
                transform.parent.GetComponent<ShowDices>().audioPlay = true;
                transform.parent.GetComponent<AudioSource>().Play();
            }

        }
    }

    IEnumerator DiceCheckCoroutine()
    {
        yield return new WaitForSeconds(2);

        while (rb.velocity.x > 0.1f || rb.velocity.y > 0.1f || rb.velocity.z > 0.1f)
        {

        }
        

        if (transform.eulerAngles.x > 360 - rotationError || transform.eulerAngles.x < 0 + rotationError)
        {
            if (transform.eulerAngles.z > 360 - rotationError || transform.eulerAngles.z < 0 + rotationError)
            {
                upFace = 6;
            }
            else if (transform.eulerAngles.z > 90 - rotationError && transform.eulerAngles.z < 90 + rotationError)
            {
                upFace = 2;
            }
            else if ((transform.eulerAngles.z > 180 - rotationError && transform.eulerAngles.z < 180 + rotationError))
            {
                upFace = 1;
            }
            else if (transform.eulerAngles.z > 270 - rotationError && transform.eulerAngles.z < 270 + rotationError)
            {
                upFace = 5;
            }
            
        }
        else if (transform.eulerAngles.x > 90 - rotationError && transform.eulerAngles.x < 90 + rotationError) 
        {
                upFace = 3;
        }
        else if ((transform.eulerAngles.x > 180 - rotationError && transform.eulerAngles.x < 180 + rotationError))
        {
            if (transform.eulerAngles.z > 360 - rotationError || transform.eulerAngles.z < 0 + rotationError)
            {
                upFace = 1;
            }
            else if (transform.eulerAngles.z > 90 - rotationError && transform.eulerAngles.z < 90 + rotationError)
            {
                upFace = 5;
            }
            else if ((transform.eulerAngles.z > 180 - rotationError && transform.eulerAngles.z < 180 + rotationError))
            {
                upFace = 6;
            }
            else if (transform.eulerAngles.z > 270 - rotationError && transform.eulerAngles.z < 270 + rotationError)
            {
                upFace = 2;
            }
            
        }
        else if (transform.eulerAngles.x > 270 - rotationError && transform.eulerAngles.x < 270 + rotationError)  
        {
                upFace = 4;
        }

        transform.parent.GetComponent<ShowDices>().enabled = true;
        transform.parent.GetComponent<Logic>().enabled = true;
        transform.parent.GetComponent<Logic>().ResetPoints();
        transform.parent.GetComponent<ShowDices>().begin++;
        transform.parent.GetComponent<Logic>().begin++;
        zoomCamera.GetComponent<SelectDice>().begin++;
        this.GetComponent<DiceRoll>().enabled = false;
        
    }
}
