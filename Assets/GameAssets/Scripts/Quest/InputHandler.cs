using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    private IQuestObject _currentQuestObject;
    
    private void OnMouseDown()
    {
        Vector2 mpos = Mouse.current.position.ReadValue();
        Vector2 mappedMPos = new Vector2(
            CustomTools.Remap(mpos.x, 0, Screen.width, 0, 1920),
            CustomTools.Remap(mpos.y, 0, Screen.height, 0, 1080)
        );
        
        if (gameObject.TryGetComponent(out IQuestObject questObject))
        {
            playerController.ClearAllSubscriptions();
            playerController.OnPlayerApproached += PlayerEndApproachHandler;
            playerController.MoveTo(mappedMPos, 50);
            _currentQuestObject = questObject;
        }
        else
        {
            playerController.ClearAllSubscriptions();
            playerController.MoveTo(mappedMPos);
        }
    }

    private void PlayerEndApproachHandler()
    {
        playerController.OnPlayerApproached -= PlayerEndApproachHandler;
        _currentQuestObject.OnPlayerApproach();
        _currentQuestObject = null;
    }
    
}
