using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float moveDistance = 3f; // ��������� ��������
    public float moveSpeed = 1f;    // �������� ��������
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // �������� ������-����� �� ��� X
        float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 1) - moveDistance;
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);
    }
}