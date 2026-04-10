using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HintLogic : MonoBehaviour
{
    [SerializeField]
    public JumblerLogic jumbler;
    public char[] unjumbled;
    public char[] author;
    [SerializeField]
    public GameObject hintBox; //need to make UI popup for hint box
    public string hintStr;
    public Text textElement;
    public char[] hintArr;
    [SerializeField]
    public SnapTarget checkTarget;
    public GameObject[] targetArr; 
    public string[] colourArr;
    public int hintCount = 0;
    string TargetColour;


    public void hints()
    {
       
        // Get the reference to the script
        //hintBox = Instantiate(hintBox);
        jumbler = FindAnyObjectByType<JumblerLogic>();
        checkTarget = FindAnyObjectByType<SnapTarget>();

        // Point localReference 
        colourArr = checkTarget.colourArr;
        unjumbled = jumbler.unjumbled;
        author = jumbler.author;


        hintCount++;
        if (hintCount == 1)
        {
            hintStr = " Author: " + new string(author);
            textElement.text = hintStr;
        } else {   
            //(check if answer is already correct)
            // change so it checks all snapTarget colours  
            CheckTargets();
            {
                for (int j = 0; j < colourArr.Length; j++)
                {  // if red, give hint
                    if (colourArr[j] != "green")
                    {
                        giveHint();
                        textElement.text = hintStr;
                        return;
                    }
                }
            }
            hintStr = "Congratulations, your solution is correct!";
            textElement.text = hintStr;
            // write to text box
        }
    }
    public void giveHint()
        //change to picka  single snapTarget 
    {
        int rand = Random.Range(0, unjumbled.Length);   // picks random index
        for (int m = 0; m < unjumbled.Length; m++)
        {    // loops hint array to check if rand is green
            if (m == rand)
            {
                if (colourArr[m] != "green")
                {
                    hintArr[m] = unjumbled[m];
                }
            }
        }
        hintStr = new string(hintArr);
        textElement.text = hintStr;
    }

    public void CheckTargets()
    {
        targetArr = GameObject.FindGameObjectsWithTag("CheckTarget");
        for (int k = 0; k < targetArr.Length; k++) {
            colourArr[k] = targetArr[k].GetComponent<SnapTarget>().snap_Colour;
        }
    }
}
