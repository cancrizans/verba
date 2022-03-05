using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VerbaOven : MonoBehaviour
{
    public Mesh sourceMesh;
    public MeshFilter targetFilter;

    public VerbaProfile bakeProfile;

    public enum OriginalColorsBehaviour {Mix, Discard};
    public OriginalColorsBehaviour originalColorsBehaviour;


    public enum TextureBehaviour {BakeOut, Ignore};



    private void Awake(){

        if(!targetFilter){
            targetFilter = GetComponent<MeshFilter>();
        }

        if(!sourceMesh){
            if(targetFilter)
                sourceMesh = targetFilter.sharedMesh;
        }




    }

    public bool CanBake(){
        if(!sourceMesh){
            Debug.LogWarning("The source mesh for this bake is null.");
            return false;
        }

        if(!sourceMesh.isReadable){
            Debug.LogWarning("The source mesh is not enable for read/write, which is necessary for the bake. Please make sure the mesh is read/write enabled through the model file's import settings.");
            return false;
        }

        if(!targetFilter){
            Debug.LogWarning("The target MeshFilter for the bake is null. This is where baked mesh will be placed.");
            return false;
        }



        return true;
    }


    public void Bake(){
        if(!CanBake())
            return;


        VerbaLight [] lights = FindObjectsOfType<VerbaLight>();

        Vector3 [] sourcePositions = sourceMesh.vertices;
        Vector3 [] sourceNormals = sourceMesh.normals;
        Color [] sourceColors = sourceMesh.colors;

        int N = sourcePositions.Length;

        bool sourceColorsExist = sourceMesh.colors.Length>0;

        Vector3 [] positions = new Vector3[N];
        Vector3 [] normals = new Vector3[N];
        Vector3 [] shading = new Vector3 [ N];

        for(int i=0; i<N ; i++){
            positions[i] = targetFilter.transform.TransformPoint(sourcePositions[i]);
            normals[i] = targetFilter.transform.TransformVector(sourceNormals[i]).normalized;
            shading[i] = Vector3.zero;
        }

        foreach(VerbaLight light in lights){
            for(int i=0; i<N ; i++){
                if(!light.InRange(positions[i],normals[i]))
                    continue;

                shading[i] += light.SampleAttenuation(positions[i],normals[i],bakeProfile);
            }
        }

        for(int i=0; i<N ; i++){
            //convert back to gamma
            switch(bakeProfile.colorSpace){
                case VerbaUtilities.ColorSpace.Gamma:
                    break;
                case VerbaUtilities.ColorSpace.Linear:
                    shading[i] = VerbaUtilities.LinearToGamma(shading[i]);
                    break;
                
            }

            if(sourceColorsExist && originalColorsBehaviour == OriginalColorsBehaviour.Mix)
                shading[i] = new Vector3(shading[i].x * sourceColors[i].r, shading[i].y * sourceColors[i].g,shading[i].z * sourceColors[i].b);


        }

        Color [] bakedColors = new Color[N];
        for(int i=0; i<N ; i++){
            bakedColors[i] = new Color(shading[i].x,shading[i].y,shading[i].z,1);
        }

        Mesh bakedMesh = Instantiate(sourceMesh);
        bakedMesh.colors = bakedColors;

        targetFilter.mesh = bakedMesh; 

    }
}
