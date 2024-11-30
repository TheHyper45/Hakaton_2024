using UnityEngine;

static class TransformExtensions {
    public static Vector2 Position2D(this Transform transform) {
        return new Vector2(transform.position.x,transform.position.y);
    }
}

static class MathEx {
    public static Vector2 Floor(Vector2 pos) {
        return new Vector2(Mathf.Floor(pos.x),Mathf.Floor(pos.y));
    }
}