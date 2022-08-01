using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public int NumPar = 10;
    public float visibleTime = 2.1f;
    public GameObject[] particle_prefabs;
    void Start()
    {
        for(int i=0;i<NumPar;i++)
        {
            int index = Random.Range(0,particle_prefabs.Length);
            GameObject PPrefabs = Instantiate(particle_prefabs[index]);
            PPrefabs.transform.position = transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        visibleTime -= Time.deltaTime;
        if(visibleTime <= 0) {
            Destroy(gameObject);
        }
        
    }
}
