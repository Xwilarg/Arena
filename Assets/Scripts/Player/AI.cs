using UnityEngine;

public class AI : MonoBehaviour
{
    private Character _character;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        var characters = AIManager.S.GetCharacters(_character);

        var player = characters[0];

        // Draw a square at the player position
        var tmp = player.transform.position / .64f;
        var playerPos = new Vector3((int)tmp.x, (int)tmp.y, (int)tmp.z) * .64f + new Vector3(.64f * (player.transform.position.x < 0 ? -1 : 0), .64f * (player.transform.position.y < 0 ? -2 : -1));
        DebugUtils.DrawSquare(playerPos, .64f, Color.blue);
    }
}
