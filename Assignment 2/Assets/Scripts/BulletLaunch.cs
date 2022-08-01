using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    public Vector3 shootingDir;
    public float force = 900f;
    public float visibleTime = 2f;
    public GameObject Explosion_prefab;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(shootingDir* force);
    }

    // Update is called once per frame
    void Update()
    {
        visibleTime -= Time.deltaTime;
        if(visibleTime <= 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter (Collision collision){
        Debug.Log(collision.transform.name);
        if(collision.transform.tag == "CollisionWall") {
            Debug.Log("Collison with the wall");
            GameObject particlesObj = Instantiate (Explosion_prefab);
            particlesObj.transform.position = transform.position;
        }
    }
}
