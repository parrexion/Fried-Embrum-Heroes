﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MapTile : MonoBehaviour {

	public MapCreator mapCreator;
	public TacticsMove currentCharacter;
	public GameObject highlight;

	[Header("Selectable")]
	public bool current;
	public bool target;
	public bool pathable;
	public bool selectable;
	public bool attackable;
	public bool supportable;
	public bool reachable;

	[Header("Map values")]
	public int posx;
	public int posy;
	public TerrainTile terrain;

	[Header("Distance Search")]
	public int distance = 1000;
	public MapTile parent;

	private SpriteRenderer _rend;


	// Use this for initialization
	private void Start () {
		_rend = highlight.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	private void Update () {
		SetHighlightColor();
	}

	public void PrintPos() {
		Debug.Log("Pos - x: " + posx + " , posy: " + posy);
	}

	private void SetHighlightColor() {
		Color tileColor = Color.white;
		tileColor.a = 0.35f;

		if (current) {
			tileColor = Color.magenta;
			tileColor.a = 0.35f;
		}
		else if (target) {
			tileColor = Color.cyan;
			tileColor.a = 0.35f;
		}
		else if (pathable) {
			tileColor = Color.yellow;
			tileColor.a = 0.35f;
		}
		else if (selectable) {
			tileColor = Color.blue;
			tileColor.a = 0.35f;
		}
		else if (attackable) {
			tileColor = Color.red;
			tileColor.a = 0.35f;
		}
		else if (supportable) {
			tileColor = Color.green;
			tileColor.a = 0.35f;
		}
		else if (reachable) {
			tileColor = Color.yellow;
			tileColor.a = 0.35f;
		}
		else {
			tileColor.a = 0f;
		}

		_rend.color = tileColor;
	}

	public void SetTerrain(TerrainTile terrainData) {
		terrain = terrainData;
		SpriteRenderer spr = GetComponent<SpriteRenderer>();
		spr.sprite = terrainData.sprite;
		spr.color = terrainData.tint;
	}

	public int GetRoughness(ClassType type) {
		for (int i = 0; i < terrain.canMoveTypes.Length; i++) {
			if (terrain.canMoveTypes[i].type == type)
				return terrain.canMoveTypes[i].roughness;
		}
		Debug.LogError("Forgot to add type " + type);
		return 1;
	} 

	public void Reset() {
		current = false;
		target = false;
		pathable = false;
		selectable = false;
		attackable = false;
		supportable = false;

		distance = 1000;
		parent = null;
	}

	public void FindNeighbours(Queue<MapTile> progress, int currentDistance, TacticsMove tactics, int moveSpeed, bool showAttack, bool isDanger) {
		MapTile tile = mapCreator.GetTile(posx-1, posy);
		if (CheckTile(tile, currentDistance, moveSpeed, tactics.faction, tactics.stats.charClass.classType, tactics.GetWeapon(), tactics.GetSupport(), showAttack, isDanger))
			progress.Enqueue(tile);
		tile = mapCreator.GetTile(posx+1, posy);
		if (CheckTile(tile, currentDistance, moveSpeed, tactics.faction, tactics.stats.charClass.classType, tactics.GetWeapon(), tactics.GetSupport(), showAttack, isDanger))
			progress.Enqueue(tile);
		tile = mapCreator.GetTile(posx, posy-1);
		if (CheckTile(tile, currentDistance, moveSpeed, tactics.faction, tactics.stats.charClass.classType, tactics.GetWeapon(), tactics.GetSupport(), showAttack, isDanger))
			progress.Enqueue(tile);
		tile = mapCreator.GetTile(posx, posy+1);
		if (CheckTile(tile, currentDistance, moveSpeed, tactics.faction, tactics.stats.charClass.classType, tactics.GetWeapon(), tactics.GetSupport(), showAttack, isDanger))
			progress.Enqueue(tile);
	}

	public bool CheckTile(MapTile checkTile, int currentDistance, int moveSpeed, Faction faction, ClassType classType, WeaponSkill weapon, SupportSkill support, bool showAttack, bool isDanger) {
		if (checkTile == null)
			return false;

		if (checkTile.currentCharacter != null) {
			if (checkTile.currentCharacter.faction != faction)
				return false;
		}

		if (checkTile.GetRoughness(classType) == -1)
			return false;
		currentDistance += checkTile.GetRoughness(classType);
		if (currentDistance >= checkTile.distance)
			return false;

		checkTile.distance = currentDistance;
		if (isDanger) {
			checkTile.reachable = true;
		}
		else if (currentDistance <= moveSpeed && checkTile.currentCharacter == null) {
			checkTile.selectable = (checkTile.currentCharacter == null);
		}
			
		if (weapon != null && showAttack) {
			mapCreator.ShowAttackTiles(checkTile, weapon, faction, isDanger);
		}
			
		if (support != null && showAttack) {
			mapCreator.ShowSupportTiles(checkTile, support, faction, isDanger);
		}
		
		checkTile.parent = this;
		return true;
	}

}
