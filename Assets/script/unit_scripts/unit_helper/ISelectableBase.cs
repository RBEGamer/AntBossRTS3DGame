// ISelectableBase
// 
// Interface selectable objects
// 
//
//
// last change:  2015/06/17
//               first iteration - Kevin 
// 
using UnityEngine;
using System.Collections;

public interface ISelectableBase
{
	void OnSelected();
	
	void OnUnselected();
	
	bool isSelected();
	
	string getName();
}
