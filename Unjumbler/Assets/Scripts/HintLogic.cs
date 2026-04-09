using UnityEngine;

public class HintLogic : MonoBehaviour
{
    public JumblerLogic jumbler;
    public char[] unjumbled;
    public char[] author;
    public char[] jumbled;
    public bool allCaps = true; //***FOR CAPITALIZATION LOGIC***
    public char[] upperArr; //***FOR CAPITALIZATION LOGIC***
    public GameObject hintBox; //need to make UI popup for hint box
    public string hintStr;
    public char[] hintArr;
    public SnapTarget checkTarget;
    public string[] colourArr;
    public int hintCount = 0;


    public string hint()
    {
        // Get the reference to the script
        checkTarget = FindAnyObjectByType<SnapTarget>();

        // Point localReference to the same array in ScriptA
        colourArr = checkTarget.colourArr;

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
            return hintStr;

        }
        else
        { //(check if answer is already correct)
            for (int k = 0; k > colourArr.Length; k++)
            {
                if (colourArr[k] != "green")
                {  // if red, give hint
                    giveHint();
                    return hintStr;
                }
            }
            hintStr = "Congratulations, your solution is correct!";
            return hintStr;
            // write to text box
        }
    }
    public string giveHint()
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
        return hintStr;
    }
}
