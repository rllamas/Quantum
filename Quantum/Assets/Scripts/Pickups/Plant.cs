using UnityEngine;
using System.Collections;

public class Plant : Pickup {
	
	public GameObject pastPlant; // Plant object used in the past era.
	public GameObject futurePlant; // Plant object use in the future era.
		
	
	
	
	public override void Start() {
		base.Start();
		pastPlant.transform.localPosition = Vector3.zero;
		futurePlant.transform.localPosition = Vector3.zero;
		
		HandleEra();
	}
	
	
	
	
	public override void Update() {
		base.Update();
		HandleEra();
	}
	
	
	
	
	/* Sets the plant object being used based on the time era. */
	private void HandleEra() {
		if (Vortex.isPast) {
			pastPlant.SetActive(true);	
			futurePlant.SetActive(false);	
		}
		else {
			pastPlant.SetActive(false);	
			futurePlant.SetActive(true);	
		}
	}
		
	
	

}
