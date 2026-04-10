using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InfiniteLetter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* Variables */
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Transform noGrouping;

    [SerializeField]
    private GameObject infinteLetter;
    [SerializeField]
    private TMP_Text infiniteLetterText;
    [SerializeField]
    private GameObject dragPrefab;

    private DragObject currentLetter;

    /* Exactly the same as the code in DragObject's awake function as we are doing
     * The exact same things on a different prefab */
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();

        infiniteLetterText = GetComponentInChildren<TMP_Text>();
    }

    /* On the start of our drag we instantiate the draggableLetter as a copy with
     * the same characteristics of the infiniteLetter prefab because the grid layout
     * group is setting the size of objects instantiated under it and we aren't
     * instantiating as part of the group so the size needs to be set aswell. */
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Alpha Change

        GameObject instance = Instantiate(dragPrefab, infinteLetter.transform);
        instance.GetComponent<RectTransform>().sizeDelta = rectTransform.sizeDelta; // set the size to get around the layout group
        
        instance.GetComponentInChildren<TMP_Text>().text = infiniteLetterText.text; // set the text

        /* We set the current letter to our instance's dragObject to get the event data
         * we need for the dragobject script */
        currentLetter = instance.GetComponent<DragObject>();

        // Check if we are dragging something incase errors and then send the event data
        if (currentLetter != null)
        {
            currentLetter.OnBeginDrag(eventData);
        }
    }

    /* for OnDrag and OnEndDrag we are just feeding information to the current letter
     * so that DragObject can handle all the dragging */
    public void OnDrag(PointerEventData eventData)
    {
        if(currentLetter != null)
        {
            currentLetter.OnDrag(eventData);
        }
    }

    // See comment above
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentLetter != null)
        {
            canvasGroup.alpha = 1f; // set alpha back to 1

            currentLetter.OnEndDrag(eventData);

            currentLetter = null;
        }
    }

}
