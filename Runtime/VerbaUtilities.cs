using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VerbaUtilities 
{
    public enum ColorSpace {Linear, Gamma};

    private const float gamma = 2.2f;
    private const float invgamma = 1/2.2f;
    public static Vector3 GammaToLinear(Vector3 v){
        return new Vector3(Mathf.Pow(v.x,gamma),Mathf.Pow(v.y,gamma),Mathf.Pow(v.z,gamma));
    }
    public static Vector3 LinearToGamma(Vector3 v){
        return new Vector3(Mathf.Pow(v.x,invgamma),Mathf.Pow(v.y,invgamma),Mathf.Pow(v.z,invgamma));
    }
}
