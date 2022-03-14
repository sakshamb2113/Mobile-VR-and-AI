using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject cubePrefab;

    void Start()
    {
        for(int i =0;i<5;i++)
        {
        	GameObject cubeObject = Instantiate(cubePrefab);
       		cubeObject.transform.position = new Vector3 (
       				Random.Range(-15f, 15f),
       				10,
       				Random.Range(-15f, 15f)
       			);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
