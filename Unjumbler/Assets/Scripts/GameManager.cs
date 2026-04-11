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
        oddLevel = false;
    }

}
