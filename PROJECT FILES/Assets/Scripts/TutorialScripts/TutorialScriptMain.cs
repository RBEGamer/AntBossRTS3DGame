using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialScriptMain : MonoBehaviour {

	public int tutorialStep = 0;

	public base_manager baseManager;

	public Text text;
	[SerializeField]
	public List<string> tutorialTexts;

	public void Start() {
		if(text) {
		text.text = tutorialTexts[tutorialStep];
		}
	}

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
		if(text) {
		text.text = tutorialTexts[tutorialStep];
		}

		if(tutorialStep == 3) {
			baseManager.bought_scout_ants += 3;
		}
		                    
	}
}
