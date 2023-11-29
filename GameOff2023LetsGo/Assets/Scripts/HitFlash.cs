using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] Material hitMaterial;
    Material[] hitMaterials;
    SkinnedMeshRenderer[] skinnedRenderers;
    Material[][] originalMaterials;

    private void Start()
    {
        // Find all SkinnedMeshRenderers in the hierarchy
        skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Store the original materials of each submesh of each SkinnedMeshRenderer
        originalMaterials = new Material[skinnedRenderers.Length][];
        for (int i = 0; i < skinnedRenderers.Length; i++)
        {
            originalMaterials[i] = skinnedRenderers[i].materials;
        }
        foreach (SkinnedMeshRenderer skinnedRenderer in skinnedRenderers)
        {
            hitMaterials = new Material[skinnedRenderer.sharedMaterials.Length];
        }
    }

    public void EnemyHitFlash()
    {
        // Start the EnemyHitFlashCo coroutine
        StartCoroutine(EnemyHitFlashCo());
    }

    public IEnumerator EnemyHitFlashCo()
    {
        foreach (SkinnedMeshRenderer skinnedRenderer in skinnedRenderers)
        {
            

            // Assign hitMaterial to each submesh
            for (int i = 0; i < hitMaterials.Length; i++)
            {
                hitMaterials[i] = hitMaterial;
            }

            skinnedRenderer.materials = hitMaterials;
        }

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.02f);

        for (int i = 0; i < skinnedRenderers.Length; i++)
        {
            // Restore the original materials to each submesh
            skinnedRenderers[i].materials = originalMaterials[i];
        }
    }
}
