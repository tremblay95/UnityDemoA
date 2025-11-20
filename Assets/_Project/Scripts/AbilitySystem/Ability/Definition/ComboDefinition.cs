using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "New Combo", menuName = "Abilities/Combo")]
    public class ComboDefinition : ScriptableObject
    {
        [Tooltip( "Seconds to wait between completing a cast and resetting the combo index." )]
        public float resetTime = 0.2f;
        [Tooltip( "Seconds to wait after completing the combo before restarting the combo." )]
        public float cooldownTime = 0.5f;
        
        public List<AbilityDefinition> comboAbilityList;
    }
}