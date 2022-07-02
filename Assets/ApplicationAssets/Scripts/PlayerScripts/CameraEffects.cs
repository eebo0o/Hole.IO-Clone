using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
public class CameraEffects : MonoBehaviour {

    [SerializeField]
    private PostProcessingProfile profile;
    [SerializeField]
    private float adjustingTime = 1;
    
    public void ApplyDieEffect()
    {
        
        StartCoroutine(AdjustToDieEffect());
    }
    public void ApplyAwakeEffect()
    {
        StartCoroutine(AdjustToAwakeEffect());
    }
    IEnumerator AdjustToDieEffect()
    {
        ColorGradingModel.Settings settings = profile.colorGrading.settings;
        for (float t = 0.0f; t < adjustingTime; t += Time.deltaTime / adjustingTime)
        {
            settings.basic.saturation = Mathf.Lerp(profile.colorGrading.settings.basic.saturation, 0.255f, t);
            profile.colorGrading.settings = settings;
            yield return null;
        }
        profile.chromaticAberration.enabled = true;
    }

    IEnumerator AdjustToAwakeEffect()
    {
        ColorGradingModel.Settings settings = profile.colorGrading.settings;
        for (float t = 0.0f; t < adjustingTime; t += Time.deltaTime / adjustingTime)
        {
            settings.basic.saturation = Mathf.Lerp(profile.colorGrading.settings.basic.saturation, 1f, t);
            profile.colorGrading.settings = settings;
            yield return null;
        }
        profile.chromaticAberration.enabled = false;
    }
}
 