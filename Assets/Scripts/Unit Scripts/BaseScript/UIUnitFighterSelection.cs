using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUnitFighterSelection : MonoBehaviour
{
	// General
	private static List<string> passables = new List<string>() { "ground" };
	public List<GameObject> baseList;
	public List<UnitGroupFriendlyScript> unitGroupList;

	// Selection data
	public bool isAmove = false;
	public UnitGroupFriendlyScript SelectedUnitGroup = null;
	//private Vector3 rightClickPosition = -Vector3.zero;
	//private Vector3 leftClickPosition = -Vector3.zero;
	
	// Selection

	// Test UI object
	public Text myText;
	
	// Update is called once per frame
	void Update()
	{
		cleanup();

		// update selected group name
		if (SelectedUnitGroup != null)
		{
			myText.text = SelectedUnitGroup.unitGroupName;
		}
		else
		{
			myText.text = "Keine Gruppe";
		}

		// wait for proper command
		if (isAmove)
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (Input.GetMouseButtonDown(0))
				{
					if (SelectedUnitGroup != null)
					{
						SelectedUnitGroup.placeNewDefensePoint(GetDestination());
					}
					isAmove = false;
				}
				// cancel command with escape
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					isAmove = false;
				}
			}
		}
	
		// Handle mouse buttons
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			// Selection process
			if (Input.GetMouseButtonDown(0))
			{
				leftClick();
			}
			
			if (Input.GetMouseButtonDown(1))
			{
				rightClick();
			}
		}
	}
	
	// Clean positions
	private void cleanup()
	{
		if (!Input.GetMouseButtonUp(1) && !Input.GetMouseButtonUp(0))
		{
			//rightClickPosition = -Vector3.zero;
			//leftClickPosition = -Vector3.zero;
		}
	}
	
	// Handle unit selection
	private void leftClick()
	{
		if (SelectedUnitGroup != null)
		{
			SelectedUnitGroup.SendMessage("OnUnselected", SendMessageOptions.DontRequireReceiver);
			SelectedUnitGroup = null;
		}
		// Cast ray to see if object hit is selectable
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.gameObject.tag.Contains(vars.selectable_tag) &&
			    hit.transform.gameObject.tag.Contains(vars.friendly_tag))
			{
				// Send OnSelected signal to Unit / Unitgroup
				hit.transform.gameObject.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
				
				// Add unit group to selected group
				if (hit.transform.gameObject.tag.Contains(vars.unitgroup_tag))
				{
					SelectedUnitGroup = hit.transform.gameObject.GetComponent<UnitGroupFriendlyScript>();
				}
				else // Add unit's group
				{
					SelectedUnitGroup = (UnitGroupFriendlyScript)hit.transform.gameObject.GetComponent<UnitScript>().unitGroupScript;
				}
			}
		}
	}
	
	// issue rightclick command
	private void rightClick()
	{
		Vector3 rightClickPosition = GetDestination();
		if(SelectedUnitGroup != null) {
			SelectedUnitGroup.SendMessage("OnRightclick", rightClickPosition, SendMessageOptions.DontRequireReceiver);
		}
	}

	// set selected group into panic mode
	public void setPanic()
	{
		if (SelectedUnitGroup != null)
		{
			SelectedUnitGroup.setPanic();
		}
	}
	
	// check if there's a selected group, if yes wait for proper amove command
	public void doAmove()
	{
		if (SelectedUnitGroup != null)
		{
			isAmove = true;
		}
	}

	// helper function
	private Vector3 GetDestination()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit))
		{
			while (!passables.Contains(hit.transform.gameObject.name))
			{
				if (!Physics.Raycast(hit.transform.position, ray.direction, out hit))
				{
					break;
				}
				break;
			}
			
		}
		if (hit.transform != null)
		{
			return hit.point;
		}
		
		return hit.point;
	}
}
