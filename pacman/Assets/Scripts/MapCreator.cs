using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject pillPrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject homeDoor;

    [SerializeField] TextAsset mapAsset;

    private int posX = 0;
    private int posY = 0;

    private string map;

    string path;

    static List<Tile> tiles = new List<Tile>();

    private string[] mapLines;
    private void Awake()
    {
        path = Application.persistentDataPath + "/maps";
        CreateFileMap();
        ReadFileMap();
    }
    void Start()
    {
        CreateMap();
        Debug.Log(Application.persistentDataPath);
    }

    public void CreateFileMap()
    {
        Directory.CreateDirectory(path);
        File.WriteAllText(path + "/mapa.txt", mapAsset.ToString());
    }

    public void ReadFileMap()
    {
        FileStream fs = File.OpenRead(path + "/mapa.txt");

        StreamReader sr = new StreamReader(fs);

        map = sr.ReadToEnd();

        mapLines = map.Split('\n');

        sr.Close();
        fs.Close();
    }

    public void CreateMap()
    {
        Vector2 v;
        for (int i = 0; i < mapLines.Length - 1; i++)
        {
            for (int j = 0; j < mapLines[i].Length - 1; j++)
            {
                switch (mapLines[i][j])
                {

                    case 'X':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(brickPrefab, v, Quaternion.identity);
                        tiles.Add(new Tile(TileType.WALL, v));
                        break;
                    case 'O':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(pointPrefab, v, Quaternion.identity);
                        tiles.Add(new Tile(TileType.POINT, v));
                        break;
                    case '*':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(pillPrefab, v, Quaternion.identity);
                        tiles.Add(new Tile(TileType.PILL, v));
                        break;
                    case '-':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(homeDoor, v, Quaternion.identity);
                        tiles.Add(new Tile(TileType.GHOSTHOME, v));
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public static TileType GetTileTypeByPosition(int posX, int posY)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.pos.x == posX && tile.pos.y == posY)
            {
                return tile.type;
            }
        }
        return TileType.NULL;
    }
}

