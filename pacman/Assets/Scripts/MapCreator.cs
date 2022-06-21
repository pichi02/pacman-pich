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
        File.WriteAllText(path, mapAsset.ToString());
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
                        break;
                    case 'O':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(pointPrefab, v, Quaternion.identity);
                        break;
                    case '*':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(pillPrefab, v, Quaternion.identity);
                        break;
                    case '-':
                        v = new Vector2(j + 0.5f, i + 0.5f);
                        Instantiate(homeDoor, v, Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

