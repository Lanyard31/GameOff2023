using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] bool jiggly = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void ApplyRecoil(float recoilTime, float recoilAmount)
    {
        StartCoroutine(RecoilAnimation(recoilTime, recoilAmount));
    }

    private IEnumerator RecoilAnimation(float recoilTime, float recoilAmount)
    {
        float elapsedTime = 0f;

        recoilTime = recoilTime - 0.2f;

        while (elapsedTime < recoilTime)
        {
            float percentComplete = elapsedTime / recoilTime;

            // Calculate recoil position
            Vector3 recoilPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - recoilAmount * Mathf.Sin(percentComplete * Mathf.PI));

            // Add a slight jiggle (optional)
            if (jiggly)
            {
                recoilPosition += new Vector3(Random.Range(-0.008f, 0.008f), Random.Range(-0.008f, 0.008f), Random.Range(-0.008f, 0.008f));
            }

            // Apply the recoil position using Lerp for smoother motion
            transform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, percentComplete);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Smoothly lerp back to the original position
        float returnTime = 0.2f; // You can adjust this time for the return animation
        float returnElapsedTime = 0f;

        while (returnElapsedTime < returnTime)
        {
            float returnPercentComplete = returnElapsedTime / returnTime;

            transform.localPosition = Vector3.Lerp(originalPosition, transform.localPosition, returnPercentComplete);

            returnElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset to original position after recoil
        transform.localPosition = originalPosition;
    }
}
