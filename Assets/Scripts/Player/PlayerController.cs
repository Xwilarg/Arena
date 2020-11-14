using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    [SerializeField]
    private CharacterInfo _info;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // Movements
        var hor = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(hor * _info.Speed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
            _rb.AddForce(Vector2.up * _info.JumpHeight, ForceMode2D.Impulse);

        // Turn sprite to movement direction
        if (_rb.velocity.x < 0f) _sr.flipX = true;
        else if (_rb.velocity.x > 0f) _sr.flipX = false;
    }
}
