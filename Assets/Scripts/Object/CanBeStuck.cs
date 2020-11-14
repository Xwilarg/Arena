using UnityEngine;

public class CanBeStuck : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Throw()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
