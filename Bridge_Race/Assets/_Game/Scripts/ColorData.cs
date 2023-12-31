using UnityEngine;
    public enum ColorType
    {
        None = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Orange = 4,
        Gray = 5,
    }

    [CreateAssetMenu(menuName = "ColorData")]
    public class ColorData : ScriptableObject
    {
        [SerializeField] Material[] materials;

        public Material GetMat(ColorType colorType)
        {
            return materials[(int)colorType];
        }
}