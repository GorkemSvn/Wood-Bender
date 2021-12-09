using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace EKUtils.PostProcess
{
    public static class PostProcessUtils
    {
        public static void SetDepthOfField(this Volume ppv, float val)
        {
            DepthOfField tmp;
            ppv.profile.TryGet<DepthOfField>(out tmp);
            tmp.focusDistance.value = val;
        }

        public static void SetDepthOfField(this Camera cam, float val)
        {
            Volume vol = cam.GetComponent<Volume>();
            vol.SetDepthOfField(val);
        }

        public static void SetVignette(this Volume ppv, float val)
        {
            Vignette tmp;
            ppv.profile.TryGet<Vignette>(out tmp);
            tmp.intensity.value = val;
        }

        public static void SetVignette(this Camera cam, float val)
        {
            Volume vol = cam.GetComponent<Volume>();
            vol.SetVignette(val);
        }
    }
}