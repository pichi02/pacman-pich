using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    private static int instantiatedPills;
    private void Awake()
    {
        instantiatedPills++;
    }
    private void OnDestroy()
    {
        instantiatedPills--;
    }
    public int GetInstantiatedPills()
    {
        return instantiatedPills;
    }
}
