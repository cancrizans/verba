using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbaPointLight : VerbaFalloffLight
{
    public override bool InRange(Vector3 position, Vector3 surfaceNormal)
    {
        return GetInFalloffRange(position);
    }

    public override Vector3 SampleAttenuation(Vector3 position, Vector3 surfaceNormal, VerbaProfile profile)
    {
        if(profile.occlusionMask != 0){
            bool occluded = Physics.Raycast(
                transform.position, 
                position-transform.position, 
                maxDistance :  profile.OcclusionRayDistance((position-transform.position).magnitude), 
                layerMask : profile.occlusionMask);

            if(occluded)
                return Vector3.zero;

        }


        float d2 = SquaredDistance(position);
        float lambert = Mathf.Max(0f,Vector3.Dot((transform.position-position ), surfaceNormal)) /Mathf.Sqrt(d2);
        return GetColor(profile.colorSpace) * GetFalloff(d2) * lambert;
    }
}
