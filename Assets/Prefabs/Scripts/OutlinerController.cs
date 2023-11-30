using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutlinerAffordanceController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material[] outlinerMaterial;

    public void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        Material[] tempMaterials = new Material[meshRenderer.materials.Length + 1];
        meshRenderer.materials.CopyTo(tempMaterials, 0);
        outlinerMaterial.CopyTo(tempMaterials, 1);

        meshRenderer.materials = tempMaterials;
        
        meshRenderer.materials[1].SetFloat("_Scale", 1.2f);
        SetOutlineScale(false);
    }

    public void SetOutlineScale(bool trueFalse)
    {
        if (trueFalse)
        {
            meshRenderer.materials[1].SetFloat("_Alpha", 1);
        }
        else if (!trueFalse)
        {
            meshRenderer.materials[1].SetFloat("_Alpha", 0);
        }
    }

    /*private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Showing!");
            SetOutlineScale(true);
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("Hiding!");
            SetOutlineScale(false);
        }
    }*/
}
