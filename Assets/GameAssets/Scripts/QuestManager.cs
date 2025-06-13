using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class QuestManager : MonoBehaviour
{
    [SerializeField] private RectTransform player;
    [SerializeField] private float playerSpeed;

    private Vector2 targetPosition;
    private bool isPlayerRotated = false;
    public void Start()
    {
        InitCollider();
        
        targetPosition = player.anchoredPosition;
    }
    
    private void OnMouseDown()
    {
        var mpos = Mouse.current.position.ReadValue();
        var playerScale = player.localScale;

        if (mpos.x > player.anchoredPosition.x && isPlayerRotated)
        {
            playerScale.x *= -1;
            isPlayerRotated = false;
            Debug.Log("Player Rotated");
        } 
        else if (mpos.x < player.anchoredPosition.x && !isPlayerRotated)
        {
            playerScale.x *= -1;
            isPlayerRotated = true;
            Debug.Log("Player Rotated");
        }
        
        player.localScale = playerScale;
        
        player.anchoredPosition = mpos;
        Debug.Log(mpos);
        
        //TODO move to/lerp
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
