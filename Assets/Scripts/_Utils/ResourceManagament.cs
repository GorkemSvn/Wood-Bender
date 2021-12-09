using UnityEngine;

namespace EKUtils.ResourceManagament
{
    public static class ResourceUtils
    {
        public static GameObject LoadGameObject(string folder, string file)
        {
            return Resources.Load<GameObject>(folder + "/" + file);
        }

        public static GameObject LoadLevel(string file, bool bonus = false, string folder = "levels")
        {
            return Resources.Load<GameObject>(folder + (bonus ? "/_bonus/" : "/") + file);
        }

        public static Sprite LoadSprite(string folder, string file)
        {
            return Resources.Load<Sprite>(folder + "/" + file);
        }

        public static Material LoadMaterial(string file, string folder = "materials")
        {
            return Resources.Load<Material>(folder + "/" + file);
        }
    }
}