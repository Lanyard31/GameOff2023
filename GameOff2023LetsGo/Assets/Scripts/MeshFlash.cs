using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFlash : MonoBehaviour
{
    [SerializeField] Material hitMaterial;
    Material[] hitMaterials;
    MeshRenderer[] meshRenderers;
    Material[][] originalMaterials;

    private void Start()
    {
        // Find all MeshRenderers in the hierarchy
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // Store the original materials of each submesh of each MeshRenderer
        originalMaterials = new Material[meshRenderers.Length][];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].materials;
        }
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            hitMaterials = new Material[meshRenderer.sharedMaterials.Length];
        }
    }

    public void EnemyHitFlash()
    {
        // Start the EnemyHitFlashCo coroutine
        StartCoroutine(EnemyHitFlashCo());
    }

    public IEnumerator EnemyHitFlashCo()
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            // Assign hitMaterial to each submesh
            for (int i = 0; i < hitMaterials.Length; i++)
            {
                hitMaterials[i] = hitMaterial;
            }

            meshRenderer.materials = hitMaterials;
        }

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.02f);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            // Restore the original materials to each submesh
            meshRenderers[i].materials = originalMaterials[i];
        }

    }


    public void OnDamageTaken()
    {
        return;
        //Debug.Log("I'm hit!");
    }
}
