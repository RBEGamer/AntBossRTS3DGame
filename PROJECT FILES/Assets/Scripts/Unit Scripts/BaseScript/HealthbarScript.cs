using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthbarScript : MonoBehaviour {
    public UnitScript unitScript;

    // TODO: CUT HEALTH BAR LOGIC INTO DIFFERNT SCRIPT
    public GameObject healthBarGameobject;
    public float healthPanelOffset = 2.0f;
    public Text HealthText;
    public Slider HealthSlider;

	// Use this for initialization
	void Start () {
        if (healthBarGameobject)
        {
            healthBarGameobject.transform.SetParent(script_cache.main_canvas.transform, false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        updateHealthbar();
	}

    public void updateHealthbar()
    {
        if (unitScript.isVisible)
        {
            if (!healthBarGameobject.activeSelf)
            {
                healthBarGameobject.SetActive(true);
            }
            if (HealthText && HealthSlider)
            {
                HealthText.text = unitScript.flagScript.UnitName;
                HealthSlider.value = unitScript.healthScript.CurrentHealth / unitScript.healthScript.BaseHealth;
                Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                healthBarGameobject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            }
        }
        else
        {
            if (healthBarGameobject.activeSelf)
            {
                healthBarGameobject.SetActive(false);
            }
        }
    }
}
