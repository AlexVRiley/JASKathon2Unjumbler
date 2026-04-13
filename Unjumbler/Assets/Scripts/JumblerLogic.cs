using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using TMPro;


public class JumblerLogic : MonoBehaviour
{
    
    public char[] unjumbled;
    public char[] author;
    public char [] jumbled;
    bool test;
    public QuoteDatabase[] quoteDatabase;

    public int targetMaxChar;

    [SerializeField]
    private GameObject letterPrefab;
    [SerializeField]
    private GameObject letterSpawn;
    [SerializeField]
    private GameObject snapPrefab;
    [SerializeField]
    private GameObject snapSpawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
        instantiateSnapTarget();

    }
    void stringJumble()
    {
        int randQuote = Random.Range(0,quoteDatabase.Length);   //picks random quote and author number

        unjumbled = quoteDatabase[randQuote].quoteText.ToCharArray();
        author = quoteDatabase[randQuote].quoteAuthor.ToCharArray();
        unjumbled = unjumbled.Concat(" -").ToArray();
        unjumbled = unjumbled.Concat(author).ToArray();
        jumbled = new char[unjumbled.Length];

        for (int j = 0; j <= unjumbled.Length - 1; j++)
        {
            jumbled[j] = unjumbled[j];
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

    /* Here we instantiate the Letters that will be used by the user
     * Each letter contains a text child element to compare when dropped
     * in the snapTarget Script. */
    public void instantiateDraggableLetters()
    {
        for (int x = 0; x < unjumbled.Length; x++)
        {
            GameObject instance = Instantiate(letterPrefab, letterSpawn.transform, true);
            instance.name = "Letter " + x;

            TMP_Text textComponent = instance.GetComponentInChildren<TMP_Text>();
            textComponent.text = jumbled[x].ToString();
        }
    }
    /* To format the snapTargets (blanks) properly without cutting off the words in the
     * grid layout group we need to be able to detect if the next word instantiating is
     * longer than the total length of the line we are on. */
    public void instantiateSnapTarget()
    {
        int row = 0;

        for (int x = 0; x< unjumbled.Length; x++)
        {
            /* If the first character in a row is a space skip it. We do this by setting the 
             * object inactive in the scene so we can still see the space in the hierarchy */
            if (row == 0 && unjumbled[x] == ' ')
            {
                GameObject skipInstance = Instantiate(snapPrefab, snapSpawn.transform, true);
                skipInstance.name = "Target " + x;

                TMP_Text skipText = skipInstance.GetComponentInChildren<TMP_Text>();
                skipText.text = unjumbled[x].ToString();

                skipInstance.SetActive(false); // set it inactive so it doesn't affect the layout

                continue;
            }
            // Check to see how long a word is if we are at the start.
            if (x == 0 || unjumbled[x - 1] == ' ')
            {
                int wordLength = 0;
                int temp = x;

                // We count until we hit a space.
                while (temp < unjumbled.Length && unjumbled[temp] != ' ')
                {
                    wordLength++;
                    temp++;
                }

                /* Here we are checking if the current word being instantiated doesn't fit on 
                 * the current row and if it doesn't we move it to the next row by instantiating
                 * more blanks. */
                if (row + wordLength > targetMaxChar && row > 0)
                {
                    int padding = targetMaxChar - row;
                    for (int i = 0; i < padding; i++)
                    {
                        GameObject pad = Instantiate(snapPrefab, snapSpawn.transform, true);
                        pad.name = "Padding";

                        TMP_Text padText = pad.GetComponentInChildren<TMP_Text>();
                        padText.text = " ";
                        pad.GetComponent<CanvasRenderer>().SetAlpha(0);
                    }
                    row = 0; // Resets the row when we wrap to the next line.
                }
            }

            /* Here we instantiate the snapTargets that will be used by the user contaning the text
             * we need to compare when a user snaps a letter. */
            GameObject instance = Instantiate(snapPrefab, snapSpawn.transform, true);
            instance.name = "Target " + x;

            TMP_Text textComponent = instance.GetComponentInChildren<TMP_Text>();
            textComponent.text = unjumbled[x].ToString();

            if (string.IsNullOrWhiteSpace(textComponent.text))
            {
                instance.GetComponent<CanvasRenderer>().SetAlpha(0);
            }

            // We keep track of our positon on the grid.
            row++;
            if (row >= targetMaxChar)
            {
                row = 0;
            }
        }

    }
}
   
 
