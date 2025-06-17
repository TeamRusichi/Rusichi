using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class IlyaMuromets : MonoBehaviour, IQuestObject
{
    [SerializeField] private UnityEvent _onApproach;
    
    public void OnPlayerApproach()
    {
        Debug.Log("IlyaMuromets OnPlayerApproach");
        _onApproach.Invoke();
    }
}
