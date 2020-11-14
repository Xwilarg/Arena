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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        bool isGrass = Physics2D.Raycast(transform.position, Vector2.down, _jumpDist, 1 << 8).collider?.tag == "Grass";
        // Movements
        var hor = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(hor * _info.Speed * (isGrass ? _info.GrassSpeedReductor : 1f), _rb.velocity.y);

        // Turn sprite to movement direction
        if (_rb.velocity.x < 0f) _sr.flipX = true;
        else if (_rb.velocity.x > 0f) _sr.flipX = false;
    }

    private void Update()
    {
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
    }
}
