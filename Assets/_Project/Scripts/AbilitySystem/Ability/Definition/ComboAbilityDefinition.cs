using System.Collections.Generic;
using UnityEngine;

namespace UnityDemoA
{
    [CreateAssetMenu(fileName = "New Combo", menuName = "Abilities/Combo")]
    public class ComboAbilityDefinition : AbilityDefinition
    {
        [Tooltip( "Seconds to wait between completing a cast and resetting the combo index." )]
        public float resetTime = 0.2f;
        [Tooltip( "Seconds to wait after completing the combo before restarting the combo." )]
        public float cooldownTime = 0.5f;
        
        public List<SingleAbilityDefinition> comboAbilityList;
        
        public override AbilityContext GetContext()
        {
            return new ComboAbilityContext(this);
        }
    }
}