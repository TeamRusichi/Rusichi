using System;
using TMPro;
using UnityEngine;

public class Bogatur : MonoBehaviour, IQuestObject
{
    public TMP_Text text;
    
    public void OnPlayerApproach()
    {
        Debug.Log("OnPlayerApproach");
    }
}
