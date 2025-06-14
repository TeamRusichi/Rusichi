using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class QuestManager : MonoBehaviour
{
    [SerializeField] private RectTransform player;
    [SerializeField] private float playerSpeed = 400.0f;

    private Vector2 targetPosition;
    private bool isPlayerRotated = true;
    private bool isMoving;
    public void Start()
    {
        InitCollider();
        
        targetPosition = player.anchoredPosition;
    }
    
    private void OnMouseDown()
    {
        var mpos = Mouse.current.position.ReadValue();
        targetPosition = mpos;
        var playerScale = player.localScale;

        // Handle player sprite direction
        if (mpos.x > player.anchoredPosition.x && isPlayerRotated)
        {
            playerScale.x *= -1;
            isPlayerRotated = false;
        } 
        else if (mpos.x < player.anchoredPosition.x && !isPlayerRotated)
        {
            playerScale.x *= -1;
            isPlayerRotated = true;
        }
        player.localScale = playerScale;
        
        isMoving = true;
        
        // player.anchoredPosition = mpos;
        Debug.Log(mpos);
    }

    void HandleMovement()
    {
        if(!isMoving) return;
        
        // Move the player
        player.anchoredPosition = Vector2.MoveTowards(player.anchoredPosition, targetPosition, playerSpeed * Time.deltaTime);
        
        // Stop moving if player is close to the destination
        if (Vector2.Distance(player.anchoredPosition, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
    
    void Update()
    {
        HandleMovement();
    }
    
    /// <summary>
    /// Initializes collider to entirely fit the QuestZone
    /// </summary>
    private void InitCollider()
    {
        var existingCollider = GetComponent<BoxCollider2D>();
        if(existingCollider != null) DestroyImmediate(existingCollider);
        
        var collider = gameObject.AddComponent<BoxCollider2D>();
        var rectTransform = collider.gameObject.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            collider.offset = new Vector2(0, rectTransform.rect.height / 2);
            collider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            Debug.Log($"QuestManager's collider initialized! ({collider.offset}; {collider.size})");
        }
        else
        {
            Debug.LogError("QuestManager's rectTransform is not initialized!");
        }
    }
}
