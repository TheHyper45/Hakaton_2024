using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MapBlock : MonoBehaviour {
    public enum Type { Directional,Death,Exit }
    [SerializeField]
    private Type type;
    [SerializeField]
    private Player.Direction direction;
    public Type GetBlockType() => type;
    public Player.Direction GetBlockDirection() => direction;
    [HideInInspector,NonSerialized]
    public AudioSource audioSource;
    private void Awake() => audioSource = GetComponent<AudioSource>();
}
