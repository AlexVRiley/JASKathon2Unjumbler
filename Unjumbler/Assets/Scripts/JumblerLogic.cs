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
    public letterPrefab[] letterInstants;
    int randQuote;
    public bool allCaps = true; //***FOR CAPITALIZATION LOGIC***
    public char[] upperArr; //***FOR CAPITALIZATION LOGIC***
    public GameObject hintBox; //need to make UI popup for hint box
    public string hintStr;
    public char[] hintArr;
    public SnapTarget checkTarget;
    public string[] colourArr;
    public int level;
    public int hintCount = 0;

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

        // ***************need to input selected level*********************

        if (level == 1 || level == 2) 
        {
        stringJumble();
        instantiateDraggableLetters(false);
        }

        if (level == 3 || level == 4)
        {
        instantiateDraggableLetters(true);
        }

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
            if (unjumbled[j] != ' '){ 
                hintArr[j] = '_';
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

        upperArr = jumbled.Select(char.ToUpper).ToArray(); //***FOR CAPITALIZATION LOGIC***
    }

    public void instantiateDraggableLetters(bool infiniteAlphabet)
    // Initially instantiates all draggable letters as capital letters
    {
        if (infiniteAlphabet == false) {
        // Display jumbled letters
            for (int x = 0; x < jumbled.Length; x++)
            {
                Instantiate(letterPrefab, letterSpawn.transform, true);
                letterPrefab.name = "Letter " + x;
                letterPrefab.GetComponentInChildren<TMP_Text>().text = jumbled[x].ToString().ToUpper();
                letterInstants[x] = letterPrefab;
            }
        }

        else
        // Display alphabet
        {
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!?.',;:".ToCharArray();
            for (int x = 0; x < alphabet.Length; x++)
            {
                Instantiate(letterPrefab, letterSpawn.transform, true);
                letterPrefab.name = "Letter " + x;
                letterPrefab.GetComponentInChildren<TMP_Text>().text = alphabet[x].ToString();
                letterInstants[x] = letterPrefab;
            }
        }

    }

    public void changeCapitalization()
    // Called when either the "Show Capitalization" or "Hide Capitalization" button is pressed by user
    {
        if (allCaps == true)
        {
            for (int x = 0; x < letterInstants.Length; x++)
            {
                letterPrefab.GetComponentInChildren<TMP_Text>().text = jumbled[x].ToString();
            }
            allCaps = false;
        }

        if (allCaps == false)
        {
                for (int x = 0; x < letterInstants.Length; x++)
            {
                letterPrefab.GetComponentInChildren<TMP_Text>().text = upperArr[x].ToString();
            }
            allCaps = true;
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

    public void checkAnswer()
    // Called when the "Check Attempt" button is pressed by user

    {
        SnapTarget checkTarget = colourArr<SnapTarget>(); // retrieve colourArr from SnapTarget
        
        for(int i = 0; i < colourArr.Length; i++)
        {
            if (colourArr[i] == "green")
            {
                // turn that letter's colour green
            }
            if (colourArr[i] == "red")
            {
                // turn that letter's colour red
            }
        }
    }


    public string hint() 
        {
        // Get the reference to the script
        checkTarget = FindAnyObjectByType<SnapTarget>();

        // Point localReference to the same array in ScriptA
        colourArr = checkTarget.colourArr;
        hintCount ++;
        if (hintCount == 1){ // reveal Author name}
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

        } else
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


