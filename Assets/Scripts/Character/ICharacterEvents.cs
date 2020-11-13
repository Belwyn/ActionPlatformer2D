using Belwyn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game.Character {

    public interface ICharacterEvents {

        Vector2Event onMovementChange { get; }
        BoolEvent onGroundedChange { get; }
        BoolEvent onJumpingChange { get; }
        BoolEvent onAttackChange { get; }    
        BoolEvent onDashChange { get; }
        BoolEvent onAirDashChange { get; }
        BoolEvent onClingChange { get; }

    }

}