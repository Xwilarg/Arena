using UnityEngine;

public static class DebugUtils
{
    public static void DrawSquare(Vector2 pos, float size, Color c)
    {
        Debug.DrawLine(pos, pos + Vector2.right * size, c);
        Debug.DrawLine(pos, pos + Vector2.up * size, c);
        Debug.DrawLine(pos + Vector2.right * size, pos + Vector2.one * size, c);
        Debug.DrawLine(pos + Vector2.up * size, pos + Vector2.one * size, c);

    }
}