using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AlexanderNevskiy : MonoBehaviour, IQuestObject
{
    [SerializeField] private UnityEvent onApproach;
    
    public void OnPlayerApproach()
    {
        onApproach.Invoke();
    }
}
