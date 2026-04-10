using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;



public class SnapTarget : MonoBehaviour, IDropHandler, IPointerExitHandler
{
    public string snap_Colour = "default";
    private TMP_Text snapLetterText;
    public bool isHint = false;

    /* When the user ends their drag on a draggable object we check the following: */
    public void OnDrop(PointerEventData eventData)
    {
        /* Get the specific dragged object so we can gather information from it */
        DragObject draggedItem = eventData.pointerDrag.GetComponent<DragObject>();
        if (draggedItem != null) // If there is a draggable object being dropped
        {
            /* Here we are finding the specifics for the dragged objects transform
             * alongside the DropTargets Transform */
            RectTransform draggedRectTransform = draggedItem.GetComponent<RectTransform>();
            RectTransform dropTargetRectTransform = GetComponent<RectTransform>();

            /* Now we just set the dragged objects position to the DropTargets */
            draggedRectTransform.position = dropTargetRectTransform.position;

            /* Compare draggedItem's letter to the SnapTarget's letter */
            if (snapLetterText.text == draggedItem.GetComponent<TMP_Text>().text)
            {
                snap_Colour = "green";
            }
            
            if (snapLetterText.text != draggedItem.GetComponent<TMP_Text>().text)
            {
                snap_Colour = "red";
            }

            if (oddLevel == true)
            {
                // display colour change
            }

        }
    }

    /* For updating the check on if a letter is removed from a snapTarget */
    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform dropTargetRectTransform = GetComponent<RectTransform>();
        /*RectTransform draggedRectTransform = GameObject.FindGameObjectsWithTag("DraggableTag").GetComponent<RectTransform>();

        if (dropTargetRectTransform.position != draggedRectTransform.position)
        {
            snap_Colour = "default";

                    if (oddLevel == true)
                    {
                        // display colour change
                    }

        }*/
    }


    public void ColourHint()
    {
        // set target vertex to white 
        if (isHint == false)
        {
            snapLetterText.color = new Color(255,255,255);
        }
    }
}
