using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRSpawnAlign : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform spawnPoint;

    void Start()
    {
        xrOrigin.position = spawnPoint.position;
        xrOrigin.rotation = spawnPoint.rotation;
    }
}
