using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform pacman;
    void Update()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {
        transform.position = new Vector3(pacman.transform.position.x, pacman.transform.position.y, transform.position.z);
    }
}
