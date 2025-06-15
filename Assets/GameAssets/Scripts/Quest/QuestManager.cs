using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private QuestZone questZone;
    [SerializeField] private List<IQuestObject> questObjects;

    private void Start()
    {
        questZone.InitCollider();
    }

    private void Update()
    {
        playerController.UpdateMovement();
    }
}
