using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDice : MonoBehaviour
{
    public Material selectedMaterial;
    public Material unselectedMaterial;
    public List<GameObject> rerollDices;
    public Button rerollButton;
    public Text total;
    public Text bet;
    public Text win;
    public GameObject cup;
    public Camera mainCamera;
    public Camera zoomCamera;
    public int begin;


    // Start is called before the first frame update
    void Start()
    {
        rerollButton.enabled = false;
        rerollButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (begin == 5)
        {
            rerollButton.enabled = true;
            rerollButton.gameObject.SetActive(true);


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray,out hit, 30f))
                {
                    if (hit.transform)
                    {
                        if (hit.transform.gameObject.tag == "Dice" && !rerollDices.Contains(hit.transform.gameObject))
                        {
                            rerollDices.Add(hit.transform.gameObject);
                            hit.transform.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selectedMaterial;
                        }
                        else if (hit.transform.gameObject.tag == "Dice" && rerollDices.Contains(hit.transform.gameObject))
                        {
                            rerollDices.Remove(hit.transform.gameObject);
                            hit.transform.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = unselectedMaterial;
                        }
                    }
                }
            }

            if (rerollButton.GetComponent<ButtonPress>().value == true && rerollDices.Count > 0 && Score.score > Score.bet / 2)
            {
                mainCamera.enabled = true;
                zoomCamera.enabled = false;

                Score.score -= Score.bet / 2;

                cup.GetComponent<CupShake>().Reposition();

                foreach (GameObject die in rerollDices)
                {
                    die.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = unselectedMaterial;
                    die.GetComponent<DiceRoll>().enabled = true;
                    die.GetComponent<DiceRoll>().Reposition();
                }

                rerollButton.GetComponent<ButtonPress>().value = false;
                rerollButton.enabled = false;
                rerollButton.gameObject.SetActive(false);
                total.gameObject.SetActive(false);
                bet.gameObject.SetActive(false);
                win.gameObject.SetActive(false);

                this.GetComponent<SelectDice>().enabled = false;
            }
            else if (rerollButton.GetComponent<ButtonPress>().value == true && rerollDices.Count == 0)
            {
                rerollButton.GetComponent<ButtonPress>().value = false;
            }
        }
    }
}
