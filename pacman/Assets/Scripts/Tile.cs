using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 pos;
    public TileType type;

    public Tile(TileType type, Vector2 pos)
    {
        this.type = type;
        this.pos = pos;
    }
}
