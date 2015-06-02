// ObjectBase
// 
// Interface for general unit handling
// Every unit/unit related object should inherit these to ensure functionality
//
//
// last change:  2015/05/31
//               first iteration - Kevin 
// 
using UnityEngine;
using System.Collections;

public interface ObjectBase
{
    void OnSelected();

    void OnUnselected();

    void OnRightclick(Vector3 destination);

    bool isSelected();

    string getName();

    void moveToTarget(GameObject target);

    void moveToVector3(Vector3 destination);
}
