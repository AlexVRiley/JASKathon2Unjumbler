using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using Unity.Burst.CompilerServices;

// Pseudocode for JumblerLogic.cs
// Written by Justina & K-Lyn Tues. March 31 - Tutorial Time Meeting
// To be written as real C++ code once UI and logic are linked


// "snap_isFilled" bool
// "snap_isCorrect" bool
// "snap_Colour" green, red, null
// “colourArr” []



// if (level = 1 || level = 2) {
// display stringJumble()
// }


// if (level = 3 || level = 4) {
// display infinitelyDraggableAlphabet
// }


// SnapTarget.cs
// if(snapTarget_isFilled == true)
// {
//      if (snapTarget_isCorrect == true) { // snapTarget_Colour = green }
//      else { // snapTarget_Colour = red }
//    
//      if (level = 1 || level = 3) {
//          //display snapTarget_Colour
//      }
//
//      if (level = 2 || level = 4) {
//          //only display snapTarget_Colour if user hits "Check" button
//      }
// }


/*    string hint{
        hintCount ++;
        if (hintCount = 1){ // reveal Author name}
        for (int l = 0; l > unjumbled.Length; l++) {
            if (l > unjumbled.Length - author.Length)
            {
                hintArr[i] = unjumbled[i]; //updates to show author                       
                hint = new string(hintArr);
                // need to show hint in text box
            }

        } else
        { //(check if answer is already correct)
            for (int k = 0; k > colourArr.Length; k++)
            {
                if (colourArr[k] != green)
                {  // if red, give hint
                    giveHint();
                    hint = new string(hintArr);
                    return Hint;
                }
            }
            hint = "Congratulations, your solution is correct!";
            return Hint;
            // write to text box
        } 
    }

    void giveHint()
        {
        int rand = Random.Range(0, unjumbled.Length);   // picks random index
        for (int m = 0; m < unjumbled.Length; m++)
        {    // loops hint array to check if rand is green
            if (m == rand)
                {
                    if (colourArr[m] != green)
                        {
                            hintArr[m] = unjumbled[m];
                        }
                }
            }
        }


    bool capitalize(){
        //WHEN CAPITALIZATION BUTTON PRESSED
        char[] upperArr;
        upperArr = jumbled.Select(char.ToUpper).ToArray();

        if (showCapitalization == false)
        {
            // show unjumbled
            return false;
        }
        if (showCapitalization == true)
        {
            // show upperArr
            return true;
        }
    }
*/



public class JumblerLogic : MonoBehaviour
{
    public string[] quotes = new [] {"We can only see a short distance ahead, but we " +
        "can see plenty there that needs to be done.", "That brain of mine is something " +
        "more than merely mortal; as time will show.", "The most dangerous phrase in the language is, 'We've always done it this way'.", "Hope and curiosity about the future seemed better " +
        "than guarantees", "In mathematics you don't understand things. You just get used to them.",
        "I like to learn. That's an art and a science.", "We're having an information explosion, and " +
        "it's certainly obvious that information is of no use unless it's available", "Logic is the foundation " +
        "of the certainty of all the knowledge we acquire.", "The world would be a better place if more engineers, " +
        "like me, hated technology", "I'll bet you don't have a computer in your living room."};
    public string[] person = new[] {"Alan Turing", "Ada Lovelace", "Grace Hopper",
        "Hedy Lamarr", "John von Neumann", "Katherin Johnson", "Sister Mary Kenneth Keller",
        "Leonhard Euler", "Radia Perlman", "Mary Allen Wilkes"};
    
    public char[] unjumbled;
    public int numQuote = 10;
    public char[] author;
    public char[] jumbled;
    bool test;
    int randQuote;
    public GameObject hintBox; //need to make UI popup for hint box
    public string hint;
    public char[] hintArr;

    [SerializeField]
    private GameObject letterPrefab;
    [SerializeField]
    private GameObject letterSpawn;
    [SerializeField]
    private GameObject snapPrefab;
    [SerializeField]
    private GameObject snapSpawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stringJumble();
        
        bool answer = testAnswer();
        if (answer == true)
        {
            System.Console.WriteLine("You Win!");
        }
        else
        {
            System.Console.WriteLine("Sorry, Try Again!");
        }

        instantiateDraggableLetters();

    }
    void stringJumble()
    {
        randQuote = Random.Range(0,numQuote);   //picks random quote and author number
        unjumbled = quotes[randQuote].ToCharArray();
        author = person[randQuote].ToCharArray();
        unjumbled = unjumbled.Concat(" -").ToArray();
        unjumbled = unjumbled.Concat(author).ToArray();
        jumbled = new char[unjumbled.Length];
        hintArr = new char[unjumbled.Length];

        for (int j = 0; j <= unjumbled.Length - 1; j++)
        {
            jumbled[j] = unjumbled[j];
            if (unjumbled[i] != " "){ 
                hintArr[i] = "_";
            }
        }

        //jumbles array
        for (int i = 0; i < unjumbled.Length - 1; i++)
        {
            int randI = Random.Range(0, unjumbled.Length);
            char temp = unjumbled[randI];
            if (temp != ' ' && unjumbled[randI] != ' ')
            {
                jumbled[i] = temp;
                Debug.Log(jumbled[i]);
            }
            
        }
    }

    //test if full answer is correct
    bool testAnswer()
    {
        test = false;
        for(int i = 0; i < unjumbled.Length; i++)
        {
            if (unjumbled[i] != jumbled[i])
            {
                return false;
            }
        }
        return true;
    }

    public void instantiateDraggableLetters()
    {
        for (int x = 0; x < jumbled.Length - 1; x++)
        {
            Instantiate(letterPrefab, letterSpawn.transform, true);
            letterPrefab.name = "Letter " + x;
            letterPrefab.GetComponentInChildren<TMP_Text>().text = jumbled[x].ToString();
        }
    }
    public void instantiateSnapTarget()
    {
        for (int x = 0; x < unjumbled.Length - 1; x++)
        {
            Instantiate(snapPrefab, snapSpawn.transform, true);
            snapPrefab.name = "Target " + x;
            snapPrefab.GetComponentInChildren<TMP_Text>().text = unjumbled[x].ToString();
        }
    }
}
   
 
