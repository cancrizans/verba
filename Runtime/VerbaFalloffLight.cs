using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbaFalloffLight : VerbaLight
{
    [Tooltip("Distance after which the light is cut off.")]
    public float range = 10f;
    public enum FalloffType {InverseSquaredSmooth, InverseSquared};

    public FalloffType falloffType;


    public bool GetInFalloffRange(Vector3 position){
        return (SquaredDistance(position) < range*range);
    }


    public float SquaredDistance(Vector3 position){
        return (transform.position - position).sqrMagnitude;
    }
    public float GetFalloff(float squaredDistance){
        
        switch(falloffType){
            case FalloffType.InverseSquared:
                return 1f/squaredDistance;
            case FalloffType.InverseSquaredSmooth:
                return 1f/squaredDistance - 1f/(range*range);
        }

        return 0;
    }
    

    public override void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(intensity));
        Gizmos.color = new Color(.4f,.4f,.4f,.5f);
        Gizmos.DrawWireSphere(transform.position, range);
    }

    
}
