using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Par_Explosion : MonoBehaviour
{
    public float par_force = 100f;
    public float visibleTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Dir = new Vector3 (
            Random.Range(1f,2f),
            Random.Range(-4f,4f),
            Random.Range(-5f,5f)
        ).normalized;

        float rForce = Random.Range(0f,par_force);
        GetComponent <Rigidbody> ().AddForce(Dir*rForce);
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
