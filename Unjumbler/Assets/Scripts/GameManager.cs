using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public JumblerLogic jumbler;
    public void Easy()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
        jumbler.instantiateSnapTarget();
    }

    public void Medium()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
        jumbler.instantiateSnapTarget();
    }

    public void Hard()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
        jumbler.instantiateSnapTarget();
    }

    public void Expert()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
        jumbler.instantiateSnapTarget();
    }

    public void ExitButton()
    {

    }

    public void Hint()
    {

    }
    //TODO:
    /* -Old letters removed on moving back to main menu
     * -Upper and lower case button linked to logic 
     * -Check button needs to be linked to logic 
     * -Hint Button linked to logic */
}
