using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbaBakeMaster : MonoBehaviour
{
    public enum BakeMasterBehaviour {BakeChildren, BakeEverything};
    [Tooltip("Whether to trigger a bake on all VerbaOvens on children of this transform or on all VerbaOvens in the scene.")]
    public BakeMasterBehaviour behaviour;

    public void Bake(){
        VerbaOven [] ovens = GetComponentsInChildren<VerbaOven>(includeInactive : false);

        foreach(VerbaOven oven in ovens)
            oven.Bake();
    }
}
