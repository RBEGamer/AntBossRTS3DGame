// UnitSelectionManager
// 
// General unit selection & command handling
// Attached to camera/manager object
//
//
// last change:  2015/05/31
//               first iteration - Kevin 
// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UnitSelectionManager : MonoBehaviour
{
    // General
    public UnitGroupBase SelectedUnitGroup = null;
    //private Vector3 rightClickPosition = -Vector3.zero;
    //private Vector3 leftClickPosition = -Vector3.zero;

    // Selection
    //private int SelectionModifier = -1;
    private static List<string> passables = new List<string>() { "ground" };
    

    // Update is called once per frame
    void Update()
    {
        cleanup();

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Selection process
            if (Input.GetMouseButtonDown(0))
            {
                unitSelection();
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
    private void unitSelection()
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
                    SelectedUnitGroup = hit.transform.gameObject.GetComponent<UnitGroupBase>();
                }
                else // Add unit's group
                {
                    SelectedUnitGroup = hit.transform.gameObject.GetComponent<UnitBase>().unitGroup;
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
