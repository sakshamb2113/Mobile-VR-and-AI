using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Police_enemy : Agent
{
    // Start is called before the first frame update
    Rigidbody rBody;
    public Transform[] Target;
    private int[] check;
    private Vector3[] pos;
    private int n;
    public float force_m=3.0f;
    private float time_remaining=15;
    public Transform Enemy;
    void Start()
    {
        rBody=GetComponent<Rigidbody>();
        n=Target.Length;
        check= new int[n];
        pos= new Vector3[n+1];
        for(int i=0;i<n;i++)check[i]=1;
    }

    public override void OnEpisodeBegin(){
        
        this.rBody.angularVelocity=Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition=new Vector3(0,0.5f,0);
        
        
        while(true){
            for(int i=0;i<n+1;i++){
                pos[i]=new Vector3(Random.value*12-6, 0.5f,Random.value * 12 -6);
            }
            int tmp=1;
            for(int i=0;i<n;i++){
                for(int j=i+1;j<n+1;j++){
                     float disToTarget =Vector3.Distance(pos[i], pos[j]);
                     if(disToTarget <4){
                         tmp=0;
                         break;
                     }
                }
            }
            if(tmp==1)break;
        }
        for(int i=0;i<n;i++){
        Target[i].gameObject.GetComponent<Renderer>().material.color=Color.red;
        Target[i].localPosition=pos[i];
        check[i]=1;
        }
        Enemy.localPosition=pos[n];
        time_remaining=15;
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(Enemy.localPosition);
        for(int i=0;i<n;i++)
        sensor.AddObservation(Target[i].localPosition);
        sensor.AddObservation(this.transform.localPosition);

        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers){
        Vector3 controlSignal= Vector3.zero;

        controlSignal.x=actionBuffers.ContinuousActions[0];
        controlSignal.z=actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal.normalized * force_m);
        int flag=1;
        float val=0.5f;
        float disToTarget =Vector3.Distance(this.transform.localPosition, Enemy.localPosition);
        time_remaining-=Time.deltaTime;
        if( disToTarget < 1.42f){
           // SetReward(-2.0f);
            EndEpisode();
    
        }
        else{
            for(int i=0;i<n;i++){
                if(check[i]==0)continue;
                disToTarget =Vector3.Distance(this.transform.localPosition, Target[i].localPosition);

                if(disToTarget < 1.42f){
                // Destroy(this.Target[i]);
                    Target[i].gameObject.GetComponent<Renderer>().material.color=Color.black;
                    check[i]=0;
                    SetReward(val);
                    val+=0.5f;
                
                }
                else if(this.transform.localPosition.y<0){
                  //  SetReward(-2.0f);
                    flag=0;
                    EndEpisode();
                }
                else{
                    flag=0;
                }
            }
            if(flag==1){
                SetReward(3.5f);
                EndEpisode();
                
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        var conActionOut= actionsOut.ContinuousActions;
        conActionOut[0]=Input.GetAxis("Horizontal");
        conActionOut[1]=Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
