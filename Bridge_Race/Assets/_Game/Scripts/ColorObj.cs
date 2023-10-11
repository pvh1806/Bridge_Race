using UnityEngine;

namespace Scriptable
{
    public class ColorObj : MonoBehaviour
    {
        [SerializeField] private ColorData colorData;
        [SerializeField]  private MeshRenderer meshRenderer;
        public ColorType color;
        public void ChangeColor(ColorType colorType)
        {
            color = colorType;
            meshRenderer.material = colorData.GetMat(colorType);
        }
    }

}