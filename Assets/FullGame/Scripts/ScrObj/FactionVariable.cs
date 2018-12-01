﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction { PLAYER,ENEMY,ALLY,NONE }

[CreateAssetMenu(menuName = "Variables/Faction")]
public class FactionVariable : ScriptableObject {

	public Faction value;
}
