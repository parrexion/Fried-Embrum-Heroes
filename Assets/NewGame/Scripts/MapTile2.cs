using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MapTile2 : MonoBehaviour {

	[Header("References")]
	public MapCreator2 mapCreator;
	public GameObject highlight;

	[Header("Map values")]
	public int posx;
	public int posy;

	[Header("Selectable")]
	public bool target;

	private SpriteRenderer _rendHighlight;


	private void Start () {
		_rendHighlight = highlight.GetComponent<SpriteRenderer>();
	}
	
	private void Update () {
		SetHighlightColor();
	}

	private void SetHighlightColor() {
		Color tileColor = Color.white;

		if (target) {
			tileColor = Color.green;
			tileColor.a = 0.35f;
		}
		else {
			tileColor.a = 0f;
		}

		_rendHighlight.color = tileColor;
	}


	public void Reset() {
		target = false;
	}

}
