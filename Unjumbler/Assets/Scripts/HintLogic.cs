using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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
    private GameObject colorChange;


    public void hints()
    {

        // Get the reference to the script
        //hintBox = Instantiate(hintBox);
        jumbler = FindAnyObjectByType<JumblerLogic>();
        //checkTarget = FindAnyObjectByType<SnapTarget>();

        // Point localReference 
        //colourArr = checkTarget.colourArr;
        //unjumbled = jumbler.unjumbled;
        author = jumbler.author;
        hintCount++;
        if (hintCount == 1)
        {
            hintStr = " Author: " + new string(author);
            textElement.text = hintStr;
        }
        else
        {
            //(check if answer is already correct)
            // change so it checks all snapTarget colours  
            Debug.Log(":c");

            CheckTargets();
            Debug.Log(targetArr[0].GetComponent<SnapTarget>().snap_Colour);



            for (int j = 0; j < colourArr.Length; j++)
            {  // if red, give hint
                if (colourArr[j] != "green")
                {
                    giveHint();
                    return;

                }
                else
                {
                    hintStr = "Congratulations, your solution is correct!";
                    // write to text box
                }
            }
        }
    }
    
    
    public void giveHint()
        //change to picka  single snapTarget 
    {
        int rand = Random.Range(0, targetArr.Length);   // picks random index
        for (int m = 0; m < targetArr.Length; m++)
        {    // loops hint array to check if rand is green
            if (m == rand)
            {
                if (targetArr[m].GetComponent<SnapTarget>().snap_Colour != "green")
                {
                    GameObject c = GameObject.Find("Target " + m);
                    c.GetComponent<TMP_Text>().color = Color.white;
                    
                }
            }
        }
    }

    public void CheckTargets()
    {
        int k = 0;
        targetArr = GameObject.FindGameObjectsWithTag("CheckTarget");
        colourArr = new string[targetArr.Length];
        Debug.Log(targetArr);
        foreach (GameObject CheckTarget in targetArr) {
            string currCol = targetArr[k].GetComponent<SnapTarget>().snap_Colour;
            colourArr[k] = currCol;
            Debug.Log(targetArr[k].GetComponent<SnapTarget>().snap_Colour);

            k++;

        }
    }
}
