using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            GameObject bulletObj= Instantiate(bulletPrefab);
            BulletLaunch bullet=bulletObj.GetComponent<BulletLaunch>();
            bullet.shootingDir=new Vector3(
                -3,
                Random.Range(0f,1f),
                Random.Range(-1f,1f)
            ).normalized;
        }
    }
}
