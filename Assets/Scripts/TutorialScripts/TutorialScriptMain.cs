using UnityEngine;
using System.Collections;

public class TutorialScriptMain : MonoBehaviour {

	public int tutorialStep = 0;

	public base_manager baseManager;

	
	public void updateStep() {
		tutorialStep++;
		checkStepBoni();
	}

	public void updateStep(int upperlimit) {
		if(tutorialStep < upperlimit) {
			tutorialStep++;
		}

		checkStepBoni();
	}


	public void checkStepBoni() {
		if(tutorialStep == 3) {
			baseManager.bought_scout_ants += 3;
		}
		                    
	}

}
