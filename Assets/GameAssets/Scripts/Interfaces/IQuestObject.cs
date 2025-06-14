using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RectTransform))]
public abstract class IQuestObject : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private RectTransform rectTransform;

    public abstract void OnPlayerApproach();
}