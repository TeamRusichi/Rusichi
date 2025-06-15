using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 400.0f;
    [SerializeField] private bool facingRight = true;
    
    private RectTransform rectTransform;
    private Vector2 targetPosition;
    private bool isMoving;

    public event Action OnPlayerApproached;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void MoveTo(Vector2 position)
    {
        targetPosition = position;
        isMoving = true;
        UpdateDirection(position.x > rectTransform.anchoredPosition.x);
    }
    
    public void MoveTo(Vector2 position, float approachDistance)
    {
        Vector2 currentPos = rectTransform.anchoredPosition;
        Vector2 direction = (position - currentPos).normalized;
        
        targetPosition = position - direction * approachDistance;
        
        isMoving = true;
        UpdateDirection(position.x > currentPos.x);
    }

    private void UpdateDirection(bool moveRight)
    {
        if (moveRight != facingRight)
        {
            facingRight = moveRight;
            Vector3 scale = rectTransform.localScale;
            scale.x *= -1;
            rectTransform.localScale = scale;
        }
    }

    public void UpdateMovement()
    {
        if (!isMoving) return;
        
        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition, 
            targetPosition, 
            speed * Time.deltaTime
        );
        
        if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 0.1f)
        {
            isMoving = false;
            OnPlayerApproached?.Invoke();
        }
    }

    public void ClearAllSubscriptions()
    {
        OnPlayerApproached = null;
    }
}
