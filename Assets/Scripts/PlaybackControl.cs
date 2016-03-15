﻿using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using Vectrosity;

public class PlaybackControl : MonoBehaviour {

	public bool playing = true;
	public decimal current_time = 0;
	public decimal total_time = 0;
	/*
	public decimal slider_value;
	public int tiles = 0;
	public bool drawLine;
	public TileColoringMode tileColoringMode = TileColoringMode.TileColoringNone;
	public bool trajectoriesShown;
	public float threshold;
	bool lineIsDrawn;

	public struct Label {
		public Rect rect;
		public string label;
		public Label (Rect r, string s) {
			label = s;
			rect = r;
		}
	}
	*/

	public bool pressP = false;
	private int roundCounter;

	public bool inFirstRound() {
		return roundCounter == 0;
	}

	private void checkSettings() {
		AgentView agentViewComponent = GameObject.Find("CameraMode").GetComponent<AgentView>();
		CameraTour cameraTourComponent = GameObject.Find("CameraMode").GetComponent<CameraTour>();

		if (agentViewComponent.enabled && cameraTourComponent.enabled)
			throw new UnityException ("AgentView and CameraTour are both enabled, only one can be active.");

		//TODO check other settings... check here that files are existing?
	}


	void Start () {
		checkSettings ();
		roundCounter = 0;

		//TODO order GameObject hierarchy, associated scripts etc.

		//TODO move this stuff into the GUI in a nice way

		//threshold = 2.0f;

		pressP = true;
	}

	public bool getPressP() {
		//return false;
		if (pressP) {
			pressP = false;
			return true;
		}
		return false;
	}

	/*
	void OnGUI () {
		playing = GUI.Toggle(new Rect(30, 25, 100, 30), playing, " PLAY");
		current_time = (decimal) GUI.HorizontalSlider (new Rect (100, 30, 400, 30), (float) current_time, 0.0f, (float) total_time);

		string btnText = "show trajectories";
		if (trajectoriesShown) btnText = "hide trajectories";

		if (GUI.Button (new Rect (510,20,120,30), btnText)) {
			PedestrianLoader pl = GameObject.Find("PedestrianLoader").GetComponent<PedestrianLoader>();
			if (trajectoriesShown) {
				foreach (GameObject p in pl.pedestrians) {
					p.GetComponent<Pedestrian>().hideTrajectory();
				}
				trajectoriesShown = false;
			} else {
				foreach (GameObject p in pl.pedestrians) {
					p.GetComponent<Pedestrian>().showTrajectory();
				}
				trajectoriesShown = true;
			}
		}

		GeometryLoader gl = GameObject.Find ("GeometryLoader").GetComponent<GeometryLoader> ();
		Groundplane gp = gl.groundplane;

		btnText = "add line";
		if (drawLine) GUI.color = Color.red;
		if (lineIsDrawn) btnText = "remove line";

		if (GUI.Button (new Rect (640, 20, 80, 30), btnText)) {
			if (lineIsDrawn) {
				gp.removeLine();
				drawLine = false;
				lineIsDrawn = false;
			} else {
				drawLine = !drawLine;
				if (!drawLine) {
					gp.removeLine();
				}
			}
		}
		GUI.color = Color.white;

		if (tiles == 0) btnText = "colors by speed";
		if (tiles == 1) btnText = "colors by density";
		if (tiles == 2) btnText = "hide colors";

		if (GUI.Button (new Rect (730, 20, 120, 30), btnText)) {
			tiles = (tiles+1)%3;

			InfoText it = GameObject.Find ("InfoText").GetComponent<InfoText> ();
			if (it.diagram) it.removeDiagram();

			if (tiles == 0) tileColoringMode = TileColoringMode.TileColoringNone;
			if (tiles == 1) tileColoringMode = TileColoringMode.TileColoringSpeed;
			if (tiles == 2) tileColoringMode = TileColoringMode.TileColoringDensity;
		}

		if (tileColoringMode == TileColoringMode.TileColoringDensity) {
			threshold = GUI.HorizontalSlider (new Rect (730, 55, 120, 30), threshold, 0.0f, 6.0f);
			GUI.Label(new Rect (730, 70, 120, 30),"Threshold: "+System.Math.Round(threshold,2)+"/m²");
		}
	}

	public void lineDrawn() {
		drawLine = false;
		lineIsDrawn = true;
	}
	*/


	void Update () {
		if (playing) {
			try {
				current_time = (current_time + (decimal) Time.deltaTime);// % total_time; // modulo, ha ha! nobody will ever notice that this leads to current_time = 0
			
				if(current_time >= total_time) {
					roundCounter ++;
					current_time = 0;
					pressP = true;
					foreach (Pedestrian ped in GameObject.Find ("PedestrianLoader").GetComponent<PedestrianLoader> ().pedestrians)
						ped.resetPedestrian();
				}	
			} catch (DivideByZeroException) {
				current_time = 0;
			}
		}
		/*
		if (Input.GetKeyDown (KeyCode.Space)) {
			playing = !playing;
		}
		*/
	}

	/*
	//http://answers.unity3d.com/answers/22959/view.html
	public void takeScreenshot(){
		int resWidth = 3840;
		int resHeight = 2160;
		RenderTexture rt = new RenderTexture (resWidth, resHeight, 24);
		Camera camera = Camera.main;
		camera.targetTexture = rt;
		Texture2D screenShot = new Texture2D (resWidth, resHeight, TextureFormat.RGB24, false);
		camera.Render ();
		RenderTexture.active = rt;
		screenShot.ReadPixels (new Rect (0, 0, resWidth, resHeight), 0, 0);
		camera.targetTexture = null;
		RenderTexture.active = null; // JC: added to avoid errors
		Destroy (rt);
		byte[] bytes = screenShot.EncodeToPNG ();
		string filename = "Screenshots/screenshot" + (screenshotCounter ++) + ".png";
		System.IO.File.WriteAllBytes (filename, bytes);
		Debug.Log (string.Format ("Took screenshot to: {0}", filename));
	}
	*/
}
