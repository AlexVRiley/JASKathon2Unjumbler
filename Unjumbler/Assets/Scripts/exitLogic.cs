using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
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
    void exit()
    {
        SceneManager.LoadScene("MenuUI");


    }
}