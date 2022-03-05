using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbaLight : MonoBehaviour
{

    //This color is always specified by user in gamma space
    public Color color = Color.white;
    
    //The intensity is applied in the given color space (yes, it may present an inconsistent behaviour to user)
    public float intensity = 1f;


    //This color is always picked up in local space
    public Vector3 GetColor(VerbaUtilities.ColorSpace colorSpace){
        
        switch(colorSpace){
            case VerbaUtilities.ColorSpace.Gamma:
                return intensity * (Vector3) (Vector4) color;
            case VerbaUtilities.ColorSpace.Linear:
                return intensity * VerbaUtilities.GammaToLinear((Vector3) (Vector4) color);
            
                
        }
        return new Vector3(1f,0f,1f);
            
    }

    public virtual bool InRange(Vector3 position, Vector3 surfaceNormal){
        return false;
    }
    public virtual Vector3 SampleAttenuation(Vector3 position, Vector3 surfaceNormal, VerbaProfile profile){

        return Vector3.zero;

    }

    public virtual void OnDrawGizmos(){
            Gizmos.color = color;
            Gizmos.DrawWireCube(transform.position,1.2f*Vector3.one);
            Gizmos.DrawIcon(transform.position, "PointLight Gizmo", true);
            
            
    }
}
