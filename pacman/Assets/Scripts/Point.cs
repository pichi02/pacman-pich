using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private static int instantiatedPoints;

    private void Awake()
    {
        instantiatedPoints++;
    }
    private void OnDestroy()
    {
        instantiatedPoints--;
    }
    public int GetInstantiatedPoints()
    {
        return instantiatedPoints;
    }

}
