using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
	
	public string text;
	
	public void OnPointerEnter(PointerEventData eventData)
	{
        float offset = 36.0f;
        if (eventData.position.y > Screen.height - 100)
        {
            offset *= -1;
        }
		StartHover(new Vector3(eventData.position.x, eventData.position.y + offset, 0f));
	}   
	public void OnSelect(BaseEventData eventData)
	{
		StartHover(transform.position);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		StopHover();
	}
	public void OnDeselect(BaseEventData eventData)
	{
		StopHover();
	}
	
	void StartHover(Vector3 position) {
		TooltipView.Instance.ShowTooltip(text, position);
	}
	void StopHover() {
		TooltipView.Instance.HideTooltip();
	}
	
}