using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private JumblerLogic jumbler;
    private SnapTarget allSnaps;
    public void Easy()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
        jumbler.instantiateSnapTarget();
        allSnaps.setCheck(true);
    }

    public void Medium()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(false);
        jumbler.instantiateSnapTarget();
        allSnaps.setCheck(false);
    }

    public void Hard()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
        jumbler.instantiateSnapTarget();
        allSnaps.setCheck(true);
    }

    public void Expert()
    {
        jumbler.stringJumble();
        jumbler.instantiateDraggableLetters(true);
        jumbler.instantiateSnapTarget();
        allSnaps.setCheck(false);
    }
}
