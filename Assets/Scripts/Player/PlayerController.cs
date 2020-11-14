using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private const float _jumpDist = .66f;
    private bool _canDoubleJump = true;

    [SerializeField]
    private CharacterInfo _info;

    private List<GameObject> _grappable = new List<GameObject>();

    private GameObject _currentWeapon = null;

    private Vector3 _baseScale;
    private bool _lookingRight = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _baseScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        var tag = Physics2D.Raycast(transform.position, Vector2.down, _jumpDist, 1 << 8).collider?.tag;
        var isGrass = tag == "Grass";
        var isIce = tag == "Ice";
        // Movements
        var hor = Input.GetAxis("Horizontal");
        if (hor == 0f && isIce)
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(hor * _info.Speed * (isGrass ? _info.GrassSpeedReductor : 1f), _rb.velocity.y);

        // Turn sprite to movement direction
        if (_rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
            _lookingRight = false;
        }
        else if (_rb.velocity.x > 0f)
        {
            transform.localScale = new Vector3(_baseScale.x, _baseScale.y, _baseScale.z);
            _lookingRight = true;
        }
    }

    private void Update()
    {
        // Reset double jump if we are on the floor
        if (!_canDoubleJump && Physics2D.Raycast(transform.position, Vector2.down, _jumpDist, 1 << 8).collider != null)
            _canDoubleJump = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, _jumpDist, 1 << 8).collider != null)
                _rb.AddForce(Vector2.up * _info.JumpHeight, ForceMode2D.Impulse);
            else if (_canDoubleJump)
            {
                _canDoubleJump = false;
                _rb.AddForce(Vector2.up * _info.SecondJumpHeight, ForceMode2D.Impulse);
            }
        }
        if (Input.GetButtonDown("Fire1") && _grappable.Count > 0 && _currentWeapon == null)
        {
            var item = _grappable[0];
            _grappable.RemoveAt(0);
            if (item.CompareTag("Crate")) // If the object is a crate, we open it
                item.GetComponent<Crate>().Open();
            else if (_currentWeapon == null) // If it's something else, we pick it up
            {
                item.transform.parent = transform;
                item.GetComponent<Rigidbody2D>().simulated = false;
                item.GetComponent<Collider2D>().enabled = false;
                var g = item.GetComponent<Grappable>();
                item.transform.localPosition = g.Item.Position;
                item.transform.rotation = Quaternion.Euler(0f, 0f, g.Item.Rotation + (_lookingRight ? 0f : 90f));
                _currentWeapon = item;
            }
        }
        if (Input.GetButtonDown("Fire2") && _currentWeapon != null) // Throw your weapon
        {
            _currentWeapon.GetComponent<Collider2D>().enabled = true;
            _currentWeapon.transform.parent = null;
            var rb = _currentWeapon.GetComponent<Rigidbody2D>();
            rb.simulated = true;
            rb.AddForce(new Vector2(_info.ThrowingForce.x * (_lookingRight ? 1f : -1f), _info.ThrowingForce.y), ForceMode2D.Impulse);
            rb.AddTorque(_info.ThrowingTorque);

            _currentWeapon = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var component = collision.gameObject.GetComponent<Grappable>();
        if (component != null)
            _grappable.Add(component.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var component = collision.gameObject.GetComponent<Grappable>();
        if (component != null)
            _grappable.Remove(component.gameObject);
    }
}
