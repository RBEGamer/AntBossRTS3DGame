// UnitUIGroupManager
// 
// Holds unitgroups & issues commands from UI
//
//
// last change:  2015/07/05
//               first iteration - Kevin 
//				myText removes - Marcel
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UnitGroupUIManager : MonoBehaviour {
	
	public List<UnitGroupFriendly> unitGroupList;
	public UnitGroupFriendly selectedUnitGroupBase;

    public bool isAmove = false;
    private static List<string> passables = new List<string>() { "ground" };

	public void Awake(){

		this.name = vars.UnitGroupUIManager;
	}
    // Test UI object
    public Text myText;

	
	// Update is called once per frame
	void Update () {
        // wait for proper command
        if (isAmove)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
					if (selectedUnitGroupBase != null)
                    {
						selectedUnitGroupBase.placeNewDefensePoint(GetDestination());
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

        // update selected group name
		if (selectedUnitGroupBase != null)
        {
			//myText.text = selectedUnitGroupBase.unitGroupName;
        }
        else
        {
       //     myText.text = "Keine Gruppe";
        }
	}


    // set selected group into panic mode
    public void setPanic()
    {
		if (selectedUnitGroupBase != null)
        {
			selectedUnitGroupBase.setPanic();
        }
    }

    // check if there's a selected group, if yes wait for proper amove command
    public void doAmove()
    {
		if (selectedUnitGroupBase != null)
        {
            isAmove = true;
        }
    }

    // get click destination
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
