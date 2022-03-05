using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Baking Profile", menuName = "VerBa/Baking Profile", order = 1)]
public class VerbaProfile : ScriptableObject
{
    [Tooltip("The LayerMask of colliders that can occlude (cast shadow). 'Nothing' disables shadows.")]
    public LayerMask occlusionMask;
    
    [Range(0f,0.1f)]
    [Tooltip("If the distance (relative to length of cast ray) from an occluder to the target vertex is less than this, the occlusion is ignored. This is used to prevent unintended self-occlusion artifacts. Increase if you start to see isolated black spots in your meshes.")]
    public float occlusionSkinDepth = .01f;

    [Tooltip("Colorspace used for light mixing. Linear is more realistic and recommended, while Gamma imitates better the behaviour of typical real-time vertex shading.")]
    public VerbaUtilities.ColorSpace colorSpace = VerbaUtilities.ColorSpace.Linear;

    
    public float OcclusionRayDistance(float intendedDistance){
        return intendedDistance *(1f-occlusionSkinDepth);
    }
    
}
