using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public int[] zar = new int[5];
    public bool[] points = new bool[9];
    public int begin;
    public int exit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetPoints()
    {
        for (int i = 0; i < 9; i++)
        {
            points[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (begin >= 5)
        {
            int i;

            for (i = 0; i < 5; i++)
            {
                zar[i] = this.gameObject.transform.GetChild(i).gameObject.GetComponent<DiceRoll>().upFace;
                Debug.Log(zar[i]);
            }
            

            int[] diceValues = new int[7];
            int twos=0, threes=0, fives=0, sixx=0;
            
            for (i = 0; i < 5; i++)
            {
                if (diceValues[zar[i]] == 0 && zar[i]>=1&&zar[i]<=5) fives++;
                if (diceValues[zar[i]] == 0 && zar[i]>=2&&zar[i]<=6) sixx++;
                    diceValues[zar[i]]++;
               
                
                
            }
            for (i = 1; i <= 6; i++)
            {
                if (diceValues[i] == 5) points[0] = true;
                if (diceValues[i] == 4) points[1] = true;
                if (diceValues[i] == 3) threes++;
                if (diceValues[i] == 2) twos++; }
            // Five of a kind
            // if (points[0] = true) ;            
            // Four of a kind
            // if (points[1] = true) ;
            //Full house (Three of a kind and a pair)
            if (threes==1 && twos==1)points[2]= true;
            // 6 high straight Six High Straight — dice showing values from 2 through 6, inclusive.
            if (sixx == 5) points[3] = true;
            // Five High Straight — dice showing values from 1 through 5, inclusive.
            if (fives == 5) points[4] = true;
            //Three of a kind
            if (threes==1) points[5] = true;
            //Two pair
            if (twos == 2) points[6] = true;
            //One pair
            if (twos==1) points[7] = true;
            //High card
            points[8] = true;
             i = 0;
            while(!points[i] && i <= 8) { i++; }
            exit = i;

            this.GetComponent<Logic>().enabled = false;
        }
    }
}
