using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(tk2dParallaxCamera))]
public class tk2dParallaxCameraEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		tk2dParallaxCamera parallaxCamera = (tk2dParallaxCamera)target;

		if (GUILayout.Button("Reset Offsets"))
		{
			parallaxCamera.ResetOffsets();
		}
	}
}
