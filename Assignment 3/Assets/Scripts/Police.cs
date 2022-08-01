using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Police : Agent
{
    // Start is called before the first frame update
    Rigidbody rBody;
    public Transform Target;
    public float force_m=10;
    void Start()
    {
        rBody=GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin(){
        if(this.transform.localPosition.y<0){
            this.rBody.angularVelocity=Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition=new Vector3(0,0.5f,0);
        }
        Target.localPosition=new Vector3(Random.value*8-4, 0.5f,Random.value * 8 -4);
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        //sensor.AddObservation(rBody.velocity.x);
        //sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers){
        Vector3 controlSignal= Vector3.zero;

        controlSignal.x=actionBuffers.ContinuousActions[0];
        controlSignal.z=actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * force_m);

        float disToTarget =Vector3.Distance(this.transform.localPosition, Target.localPosition);

        if(disToTarget < 1.42f){
            SetReward(1.0f);
            EndEpisode();
        }
        else if(this.transform.localPosition.y<0){
            SetReward(-1.0f);
            EndEpisode();
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
