using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_controller : MonoBehaviour
{
    
    public int Jump;
    public int force;
    private Rigidbody selfRigidbody;
    void Start()
    {
	force = 5;
        selfRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
     if(Jump == 1){
	 // Updating Jump variable
         Jump = 0;
         selfRigidbody.AddForce(0, force, 0, ForceMode.Impulse);
     }
    }

    
    void Update()
    {
	// Rotating Clockwise - RightArrow
	if (Input.GetKey(KeyCode.RightArrow))  
        {  
            selfRigidbody.transform.Rotate(this.transform.up, 1);  
        }
	// Moving Forward - UpArrow
        if (Input.GetKey(KeyCode.UpArrow))  
        {  
            selfRigidbody.transform.Translate(this.transform.forward * Time.deltaTime);  
        }  
        // Rotating Anti Clockwise - LeftArrow
        if (Input.GetKey(KeyCode.LeftArrow))  
        {  
            selfRigidbody.transform.Rotate(this.transform.up, -1);  
        }  
        
        // Making it Jump - Spacebar

	if(Input.GetKeyUp(KeyCode.Space)){
         Jump = 1;
     	}
    }
}
