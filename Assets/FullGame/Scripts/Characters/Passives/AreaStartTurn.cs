﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passives/AreaStartTurn")]
public class AreaStartTurn : PassiveSkill {


    [Space(10)]
    public bool useOnSelf;
    
    protected override void UseSkill(TacticsMove user, TacticsMove enemy) { }
    protected override void RemoveEffect(TacticsMove user, TacticsMove enemy) { }

    protected override void ForEachBoost(CharacterListVariable list, TacticsMove user) {
        for (int i = 0; i < list.values.Count; i++) {
            if (list.values[i] == user && !useOnSelf)
                continue;
            int distance = MapCreator.DistanceTo(user, list.values[i]);
            if (distance <= range)
                list.values[i].ReceiveBuff(boost, true, true);
        }
    }
}
