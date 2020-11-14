using UnityEngine;

public class Bumper : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>()?.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
    }
}
