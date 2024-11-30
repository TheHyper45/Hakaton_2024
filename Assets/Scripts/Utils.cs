using UnityEngine;

static class TransformExtensions {
    public static Vector2 Position2D(this Transform transform) {
        return new Vector2(transform.position.x,transform.position.y);
    }
}

static class MathEx {
    public static Vector2 AlignToTileGrid(Vector2 pos,float size = 0.64f) {
        return new Vector2(Mathf.Floor(pos.x),Mathf.Floor(pos.y)) * size;
    }
}