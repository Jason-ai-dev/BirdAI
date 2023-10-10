using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flower : MonoBehaviour
{
   [Tooltip("The color when the flower is full")]
   public Color fullFlowerColor = new Color(1f,0f, .3f);

   [Tooltip("The color when the flower is empty")]
   public Color emptyFlowerColor = new Color(.5f, 0f, 1f);

    // The trigger collider representing the nectar
    [HideInInspector]
   public Collider nectarCollider;

   // The solid collider representing the flower petals
   private Collider flowerCollider;

   //the flower's material
   private Material flowerMaterial;

    public Vector3 FlowerUpVector
    {
        get
        {
        return nectarCollider.transform.up;
        }
    }

// the center position of the nectar collider

public Vector3 FlowerCenterPosition
{
    get
    {
        return nectarCollider.transform.position;
    }
}
    // The amount of nectar remaining in the flower
public float NectarAmount{get; private set;}

public bool HasNectar
{
    get
    {
        return NectarAmount > 0f;
    }
}
/// <summary>
/// Attempts to remove nectar from the flower.
/// </summary>
/// <param name = "amount">The amount of nectar to remove.</param>
/// <returns>The actual amount successfully removed.</returns>

public float Feed(float amount)
{
    // Track how much nectar was successfully taken (Cannot take more than is available)
    float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);

    // Subtract the nectar
    NectarAmount -= amount;

    if (NectarAmount <= 0){
        //No nectar remaining

        NectarAmount = 0;

        // Disable the flower and nectar colliders
        flowerCollider.gameObject.SetActive(false);
        nectarCollider.gameObject.SetActive(false);

        // Change flower color to indicate that it's empty

        flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);

        //Return the amount of nectar that was taken
    }
        return nectarTaken;
    
    }
    public void ResetFlower()
    {
        //Refill the nectar
        NectarAmount = 1f;

        // Enable the flower and nectar colliders
        flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);
        
        // Change the flower color to indicate that it's full.
        flowerMaterial.SetColor("_BaseColor" , fullFlowerColor);
    }
    private void Awake()
    {
        // Find the flower's mesh rendered and get the main material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        //Find flower and nectar colliders
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();
    }

}