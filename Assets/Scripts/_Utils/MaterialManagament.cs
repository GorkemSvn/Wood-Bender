using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EKUtils.MaterialManagament
{
    public static class MaterialUtils
    {
        public static string GetCode(this Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        public static Color GetColor(this string code)
        {
            Color c;
            ColorUtility.TryParseHtmlString("#" + code, out c);
            return c;
        }

        public static void SetColor(this Material mat, Color color, string shaderVariableName = null, float colorPower = 1f)
        {
            if (shaderVariableName == null) mat.color = color;
            else
            {
                mat.SetColor(shaderVariableName, color * colorPower);
            }
        }

        public static void SetColor(this Renderer rend, Color color, string shaderVariableName = null, float colorPower = 1f)
        {
            if (shaderVariableName == null) rend.material.color = color;
            else rend.material.SetColor(color, shaderVariableName, colorPower);
        }

        public static void SetIntensity(this Renderer rend, Color color, float colorPower)
        {
            rend.SetColor(color, "_EmissionColor", colorPower);
        }

        public static Material Copy(this Material mat)
        {
            Material m = new Material(mat);
            m.name = mat.name + "-generated-";
            return m;
        }

        public static void Copy(this Renderer rend)
        {
            rend.material = rend.material.Copy();
        }

        public static Material Copy(this Material mat, Color color, string shaderVariableName = null, float colorPower = 1f)
        {
            Material m = mat.Copy();

            if (shaderVariableName == null) m.color = color;
            else m.SetColor(shaderVariableName, color * colorPower);

            return m;
        }

        public static void Copy(this Renderer rend, Color color, string shaderVariableName = null, float colorPower = 1f)
        {
            rend.material = rend.material.Copy(color, shaderVariableName, colorPower);
        }
    }
}