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
    public char[] jumbled;
    public bool allCaps = true; //***FOR CAPITALIZATION LOGIC***
    public char[] upperArr; //***FOR CAPITALIZATION LOGIC***
    [SerializeField]
    public GameObject hintBox; //need to make UI popup for hint box
    public string hintStr;
    [SerializeField]
    public Text textElement;
    public char[] hintArr;
    [SerializeField]
    public SnapTarget checkTarget;
    public string[] colourArr;
    public int hintCount = 0;

    [MenuItem("Hint")]

    public void hints()
    {
       
        // Get the reference to the script
        hintBox = Instantiate(hintBox);
        jumbler = FindAnyObjectByType<JumblerLogic>();
        checkTarget = FindAnyObjectByType<SnapTarget>();

        // Point localReference 
        colourArr = checkTarget.colourArr;
        unjumbled = jumbler.unjumbled;
        author = jumbler.author;
        hintStr = jumbler.hintStr;
        colourArr = jumbler.colourArr;
    }
    public void hint()
    {
        //SceneManager.LoadScene();
        hintCount++;
        if (hintCount == 1)
        { // reveal Author name}
            for (int l = 0; l > unjumbled.Length; l++)
            {
                if (l > unjumbled.Length - author.Length)
                {
                    hintArr[l] = unjumbled[l]; //updates to show author                       

                    // need to show hint in text box
                }
            }
            hintStr = new string(hintArr);
            textElement.text = hintStr;
        }
        else
        { //(check if answer is already correct)
            for (int k = 0; k > colourArr.Length; k++)
            {
                if (colourArr[k] != "green")
                {  // if red, give hint
                    giveHint();
                    textElement.text = hintStr;
                }
            }
            hintStr = "Congratulations, your solution is correct!";
            textElement.text = hintStr;
            // write to text box
        }
    }
    public void giveHint()
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
}
