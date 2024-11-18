using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Combat Data", menuName = "Config/Player/Create Combat Data")]
    public class CombatStaticData : ScriptableObject
    {
        [field: SerializeField] public DamageSettings DamageSettings { get; private set; }
    }
}
