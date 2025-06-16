using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _startAngle;
    private bool _isRotating = true;
    private bool _hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _startAngle = transform.rotation.normalized.eulerAngles.z;
    }

    void Update()
    {
        if (_isRotating)
        {
            var direction = rb.linearVelocity.normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 120f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasHit || collision.gameObject.CompareTag("Arrow")) return;

        _isRotating = false;
        rb.isKinematic = true;
        rb.simulated = false;

        if (collision.gameObject.CompareTag("Target"))
        {
            _hasHit = true;
            BowMinigameManager.Instance.RegisterHit();
            gameObject.transform.SetParent(collision.gameObject.transform);
        }
    }
}