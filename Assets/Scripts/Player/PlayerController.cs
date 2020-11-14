using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character _character;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        _character.SetXAxis(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
            _character.Jump();

        if (Input.GetButtonDown("Fire1"))
            _character.Grab();

        if (Input.GetButtonDown("Fire2"))
            _character.Throw();
    }
}
