using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dragndropminigame : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image ArcherField;
    public Image SwordmansField;
    public Image[] Archers;
    public Image[] Swordmans;
    private RectTransform rectTransform;
    private Image selected;
    bool ArchersInField = false;
    bool SwordsmansInField = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if we clicked on any of the archers
        foreach (Image archer in Archers)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(archer.rectTransform, eventData.position, eventData.pressEventCamera))
            {
                selected = archer;
                //Debug.Log("Selected archer: " + archer.name);
                return;
            }
        }
        foreach (Image swordsman in Swordmans)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(swordsman.rectTransform, eventData.position, eventData.pressEventCamera))
            {
                selected = swordsman;
                //Debug.Log("Selected swordsman: " + swordsman.name);
                return;
            }
        }

        Debug.Log("Noone selected");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (selected != null)
        {
            //Debug.Log("Started dragging: " + selected.name);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (selected != null)
        {
            
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                selected.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector3 worldPoint);

            selected.transform.position = worldPoint;
            //Debug.Log("Dragging: " + selected.name);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (selected != null)
        {
            //Debug.Log("Stopped dragging: " + selected.name);
            
            selected = null;
            bool allArchersInField = true;
            foreach (Image archer in Archers)
            {
                if (!(AreImagesIntersecting(archer,ArcherField)))
                    {
                    allArchersInField = false;
                    Debug.Log($"Not all archers intersect the archer field");
                    break;
                }
            }

            bool allSwordsmansInField = true;
            foreach (Image swordsman in Swordmans)
            {
                if (!(AreImagesIntersecting(swordsman, SwordmansField)))
                {
                    allArchersInField = false;
                    Debug.Log($"Not all swordsmans intersect the swordsmans field");
                    break;
                }

            }
            if (allSwordsmansInField&&allArchersInField)
            {
                Debug.Log($"YOU WON GG!!!!!!!!!!");
                SceneManager.LoadScene("Level3");
            }
        }
    }
    bool AreImagesIntersecting(Image img1, Image img2)
    {
        RectTransform rect1 = img1.rectTransform;
        RectTransform rect2 = img2.rectTransform;

        Vector3[] corners1 = new Vector3[4];
        Vector3[] corners2 = new Vector3[4];
        rect1.GetWorldCorners(corners1);
        rect2.GetWorldCorners(corners2);

        Rect rectImg1 = new Rect(corners1[0].x, corners1[0].y,
                                 corners1[2].x - corners1[0].x,
                                 corners1[2].y - corners1[0].y);
        Rect rectImg2 = new Rect(corners2[0].x, corners2[0].y,
                                 corners2[2].x - corners2[0].x,
                                 corners2[2].y - corners2[0].y);
        return rectImg1.Overlaps(rectImg2);
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}