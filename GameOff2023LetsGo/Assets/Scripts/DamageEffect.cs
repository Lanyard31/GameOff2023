using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageEffect : MonoBehaviour
{
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;

    Volume mainVolume;
    Vignette vg;

    void Start()
    {
        mainVolume = GetComponent<Volume>();
        mainVolume.profile.TryGet<Vignette>(out vg);
    }

    public void ApplyDamageEffect()
    {
        StartCoroutine(DamageEffectCoroutine());
    }

    IEnumerator DamageEffectCoroutine()
    {
        float elapsedTime = 0f;
        float blendTime = 0.2f;

        while (elapsedTime < blendTime)
        {
            vg.intensity.value = Mathf.Lerp(0.1f, 0.6f, elapsedTime / blendTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(damageDuration);

        // Reset intensity smoothly
        elapsedTime = 0f;
        while (elapsedTime < (blendTime / 2))  // Adjust as needed for desired half-time to normal return
        {
            vg.intensity.value = Mathf.Lerp(0.6f, 0.1f, elapsedTime / (blendTime / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the intensity is back to the default value
        vg.intensity.value = 0.1f;
    }
}

