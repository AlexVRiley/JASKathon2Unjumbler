using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
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
    void Exit()
    {
        SceneManager.LoadScene("MenuUI");


    }
}