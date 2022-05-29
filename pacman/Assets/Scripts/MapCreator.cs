using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private int posX = 0;
    private int posY = 0;

    private string map;

    private string[] mapLines;
    // Start is called before the first frame update
    void Start()
    {
        ReadFileMap();
        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadFileMap()
    {
        FileStream fs = File.OpenRead("Assets/Map/mapa.txt");

        StreamReader sr = new StreamReader(fs);

        map = sr.ReadToEnd();

        mapLines = map.Split('\n');

        sr.Close();
        fs.Close();
    }

    public void CreateMap()
    {
        for (int i = 0; i < mapLines.Length - 1; i++)
        {
            for (int j = 0; j < mapLines[i].Length - 1; j++)
            {
                switch (mapLines[i][j])
                {

                    case 'X':
                        Vector2 v = new Vector2(j, i);
                        Instantiate(prefab, v, Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

