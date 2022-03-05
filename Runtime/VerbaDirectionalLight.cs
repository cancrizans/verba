using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbaDirectionalLight : VerbaLight
{
    
    private const float maxCastDistance = 30f;

    public override bool InRange(Vector3 position, Vector3 normal){
        return true;

    }

    public override Vector3 SampleAttenuation(Vector3 position, Vector3 surfaceNormal, VerbaProfile profile)
    {
        if(profile.occlusionMask != 0){
            
            bool occluded = Physics.Raycast(position - transform.forward * maxCastDistance, transform.forward, maxDistance : profile.OcclusionRayDistance(maxCastDistance), layerMask : profile.occlusionMask);

            if(occluded)
                return Vector3.zero;

        }

        float lambert = Mathf.Max(0f,-Vector3.Dot(transform.forward, surfaceNormal));

        

        return lambert * GetColor(profile.colorSpace);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        for(int th = 0; th<8; th++){
            float theta = th * Mathf.PI/4;
            Vector3 localRayPos = new Vector3(Mathf.Cos(theta),Mathf.Sin(theta),0f);
            Gizmos.DrawRay(transform.TransformPoint(localRayPos), 3f*transform.forward);
        }
    }
}
