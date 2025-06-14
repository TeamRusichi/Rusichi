using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(RectTransform), typeof(BoxCollider2D))]
public class QuestZone : MonoBehaviour
{
    public void InitCollider()
    {
        var existingCollider = GetComponent<BoxCollider2D>();
        var collider = gameObject.AddComponent<BoxCollider2D>();
        
        if (existingCollider != null) 
            DestroyImmediate(existingCollider);
        
        var rectTransform = GetComponent<RectTransform>();

        collider.offset = new Vector2(0, rectTransform.rect.height / 2);
        collider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }
}

/// <summary>
/// Editor logic related to this specific class.
/// </summary>
#if UNITY_EDITOR
[CustomEditor(typeof(QuestZone))]
public class QuestZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update collision box"))
        {
            ((QuestZone)target).InitCollider();
        }
    }
}
#endif