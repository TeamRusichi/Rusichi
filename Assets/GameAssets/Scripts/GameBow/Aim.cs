using UnityEngine;

public class Bow : MonoBehaviour
{
    private float _rotation_z;
    private float _arrowSpeed = 160f;

    void Start()
    {
        _rotation_z = transform.rotation.eulerAngles.z;
        GameManager.Instance.ResetGame();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.shotsFired < GameManager.MaxShots)
        {
            Shot();
            GameManager.Instance.RegisterShot();
        }

        Aim();
    }

    void Aim()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + _rotation_z);
    }

    void Shot()
    {
        GameObject child = GameObject.Find("Arrow");

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 direction = (mouseWorldPos - child.transform.position).normalized;

        GameObject arrow = Instantiate(child, transform.position, Quaternion.identity);
        arrow.transform.rotation = child.transform.rotation;
        var collider = arrow.AddComponent<BoxCollider2D>();
        collider.enabled = true;
        collider.offset = new Vector2(-1.213752f, 1.915088f);
        collider.size = new Vector2(0.9797218f, 1.049824f);
        var rigidbody = arrow.AddComponent<Rigidbody2D>();
        arrow.AddComponent<ArrowRotation>();
        rigidbody.linearVelocity = direction * _arrowSpeed;
        rigidbody.linearDamping = 0.0f;
    }
}