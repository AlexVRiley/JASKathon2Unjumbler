using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private JumblerLogic jumbler;
    private SnapTarget allSnaps;
    public static bool oddLevel;

    public void Easy()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
        jumbler.instantiateSnapTarget();
        oddLevel = true;

    }

    public void Medium()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
        jumbler.instantiateSnapTarget();
        oddLevel = false;

    }

    public void Hard()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
        jumbler.instantiateSnapTarget();
        oddLevel = true;

    }

    public void Expert()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
        jumbler.instantiateSnapTarget();
    }

    //TODO:
    /* -Old letters removed on moving back to main menu
     * -Upper and lower case button linked to logic 
     * -Check button needs to be linked to logic 
     * -Hint Button linked to logic */
        //oddLevel = false;
    }

