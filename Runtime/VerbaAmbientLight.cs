using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbaAmbientLight : VerbaLight
{
    
    public override bool InRange(Vector3 position, Vector3 surfaceNormal)
    {
        return true;
    }

    public override Vector3 SampleAttenuation(Vector3 position, Vector3 surfaceNormal, VerbaProfile profile)
    {
        return GetColor(profile.colorSpace);
    }
}
