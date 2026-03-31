using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SnapTarget : MonoBehaviour, IDropHandler
{
    private TMP_Text snapLetterText;

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
        }
    }

    /* TODO
     * - Add Variables to compare DraggedLetter's letter to the SnapTargets Letter
     * - Add Check for if the compared letter is correct
     */
}
