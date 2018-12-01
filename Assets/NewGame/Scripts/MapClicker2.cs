using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MapClicker2 : MonoBehaviour, IPointerDownHandler {
	
	public MapCreator2 mapCreator;
	

	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log("Click");
		int x = Mathf.FloorToInt(0.5f + eventData.pointerCurrentRaycast.worldPosition.x);
		int y = Mathf.FloorToInt(0.5f + eventData.pointerCurrentRaycast.worldPosition.y);

		MapTile2 tile = mapCreator.GetTile(x, y);
		mapCreator.ResetMap();
		tile.target = true;
	}
}
