﻿// UnitUIGroupManager
// 
// Holds unitgroups & issues commands from UI
//
//
// last change:  2015/05/31
//               first iteration - Kevin 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UnitGroupUIManager : MonoBehaviour {

    public List<UnitGroup> unitGroups;
    public UnitGroup selectedUnitGroup;

    public bool isAmove = false;
    private static List<string> passables = new List<string>() { "ground" };


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
                    if (selectedUnitGroup != null)
                    {
                        selectedUnitGroup.placeNewDefensePoint(GetDestination());
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
        if (selectedUnitGroup != null)
        {
            myText.text = selectedUnitGroup.name;
        }
        else
        {
            myText.text = "Keine Gruppe";
        }
	}


    // set selected group into panic mode
    public void setPanic()
    {
        if (selectedUnitGroup != null)
        {
            selectedUnitGroup.setPanic();
        }
    }

    // check if there's a selected group, if yes wait for proper amove command
    public void doAmove()
    {
        if (selectedUnitGroup != null)
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
