using UnityEngine;
using System.Collections.Generic;

static class TransformExtensions {
    public static Vector2 Position2D(this Transform transform) {
        return new Vector2(transform.position.x,transform.position.y);
    }
}

public static class ListExtensions {
    public static T RandomElement<T>(this List<T> list) {
        return list[Random.Range(0,list.Count)];
    }
}

static class MathEx {
    public static Vector2 Floor(Vector2 pos) {
        return new Vector2(Mathf.Floor(pos.x),Mathf.Floor(pos.y));
    }
}