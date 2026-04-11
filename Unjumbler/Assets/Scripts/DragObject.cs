using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragObject : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    /* Variables */
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Transform noGrouping;
    public bool isUpperInSentenceCase;

    [SerializeField]
    private GameObject draggableLetter;
    [SerializeField]
    public TMP_Text dragLetterText;

    /* I am using the awake method because it only runs once when the object it is 
     * attatched to becomes active in the scene */
    private void Awake()
    {
        /* Setting up all the variables to get the component of the object this script 
         * is attatched to. So in this case when a draggable letter instantiates it will 
         * look for its own Transform, CanvasGroup and its parent Canvas */
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>(); // The canvas is a parent so we use this

        /* Adding in the draggable letters text mesh pro text object for comparison */
        dragLetterText = GetComponentInChildren<TMP_Text>();

        /* When the Letters get instantiated we need to find the transform of an object
         * named "NoGrouping" in order for dragging to work properly */
        noGrouping = GameObject.Find("NoGrouping").transform;

        /* Added this line here to make sure the draggable letter knows that this is it's
         * reference when being instantiated by the infiniteLetter script */
        if(draggableLetter == null)
        {
            draggableLetter = this.gameObject;
        }
        if(dragLetterText.text == "a" || dragLetterText.text == "b" || dragLetterText.text == "c" || dragLetterText.text == "d" || dragLetterText.text == "e" || dragLetterText.text == "f" || dragLetterText.text == "g" || dragLetterText.text == "h" || dragLetterText.text == "i" || dragLetterText.text == "j" || dragLetterText.text == "k" || dragLetterText.text == "l" || dragLetterText.text == "m" || dragLetterText.text == "n" || dragLetterText.text == "o" || dragLetterText.text == "p" || dragLetterText.text == "q" || dragLetterText.text == "r" || dragLetterText.text == "s" || dragLetterText.text == "t" || dragLetterText.text == "u" || dragLetterText.text == "v" || dragLetterText.text == "w" || dragLetterText.text == "x" || dragLetterText.text == "y" || dragLetterText.text == "z")
        {
            isUpperInSentenceCase = false;
        }
        else
        {
            isUpperInSentenceCase = true;
        }
    }

    /* This function calls when the users clicks and drags the object this script is 
     * attached to */
    public void OnBeginDrag(PointerEventData eventData)
    {
        /* When the user begins dragging the letter we want to keep checking objects
         * below it. We also want to change the objects alpha to add some user feedback
         * into their interaction. Changing the alpha also helps the user see "under"
         * the object they are dragging. Lastly we also need to make sure the object
         * is removed from its formatting parent (LetterSpawnGroup) So it's transform
         * can be changed from the grid layout it is set to */
        canvasGroup.blocksRaycasts = false; // Allows the raycast to go through the object
        canvasGroup.alpha = 0.6f;  // Alpha change
        
        /* Remove parent and set to special empty object "NoGrouping" which is a child of
         * "GameplayUI" canvas and not a child of "LetterSpawnGroup" */
        draggableLetter.transform.SetParent(noGrouping, true);

        // Snap the letter to the users cursor
        SnapToCursor(eventData);
    }

    /* This just makes sure the object being interacted with (dragged) stays with the cursor */
    public void OnDrag(PointerEventData eventData)
    {
        /* Finds the objects position in the scene then adds the cursors position change
         * divided by the canvas scale to make sure the position stays relitive to the cursor */
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    /* This function calls when the object being dragged is "dropped" */
    public void OnEndDrag(PointerEventData eventData)
    {
        /* We reset the alpha and raycast settings we changed in OnBeginDrag */
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    /* Made this new function to make it so when the user starts dragging a letter we set the 
     * letter objects center to the center of the users cursor to make the interaction of
     * dropping the letters into the snapTargets more responsive */
    private void SnapToCursor(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) noGrouping, 
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }
}
