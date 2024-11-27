using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonPos : AttributesSync
{
    [SynchronizableField] public Vector3 position;
    Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int index, Quaternion rot)
        
    {
        GameObject canon = spawner.Spawn(index, position, Quaternion.identity);
        canon.transform.position = position;
    }


}
