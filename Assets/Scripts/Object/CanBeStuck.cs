using UnityEngine;

public class CanBeStuck : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _coll;
    private bool _canBeStuck = false;

    private Collider2D _playerCollider = null;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
    }

    public void Grab()
    {
        if (_playerCollider != null)
            Physics2D.IgnoreCollision(_playerCollider, _coll, false);
        _playerCollider = null;

        _rb.constraints = RigidbodyConstraints2D.None;
    }

    public void Throw(Collider2D playerColl)
    {
        _playerCollider = playerColl;
        _canBeStuck = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_canBeStuck && collision.collider.gameObject.layer == 8)
        {
            Physics2D.IgnoreCollision(_playerCollider, _coll, false);
            _playerCollider = null;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
