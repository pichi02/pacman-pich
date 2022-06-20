using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Dectivate()
    {
        gameObject.SetActive(false);
    }
}
