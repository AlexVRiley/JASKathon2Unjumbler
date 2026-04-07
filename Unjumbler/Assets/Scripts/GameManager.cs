using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public JumblerLogic jumbler;
    public void Easy()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
    }

    public void Medium()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
    }

    public void Hard()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
    }

    public void Expert()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
    }
}
