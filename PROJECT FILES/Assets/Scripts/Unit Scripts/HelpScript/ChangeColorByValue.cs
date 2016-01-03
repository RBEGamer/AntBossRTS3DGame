using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script that Lerp the color of a image depending of the scale of the transform
public class ChangeColorByValue : MonoBehaviour
{
    public Slider targetSlider;

    // Target
    public Image image;

    // Parameters
    public float minValue = 0.0f;
    public float maxValue = 1.0f;
    public Color minColor = Color.red;
    public Color maxColor = Color.green;

    // The default image is the one in the gameObject
    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    void Update()
    {
        image.color = Color.Lerp(minColor,
                                         maxColor,
                                         Mathf.Lerp(minValue,
                           maxValue,
                           targetSlider.value));
    }
}