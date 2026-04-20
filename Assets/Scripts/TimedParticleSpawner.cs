using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedParticleSpawner : MonoBehaviour
{
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnParticles());
    }
IEnumerator SpawnParticles()
{
    yield return new WaitForSeconds(1f);
    ps.Emit(1);

     yield return new WaitForSeconds(29f);
    ps.Emit(1);

     yield return new WaitForSeconds(60f);
    ps.Emit(1);
}
}
