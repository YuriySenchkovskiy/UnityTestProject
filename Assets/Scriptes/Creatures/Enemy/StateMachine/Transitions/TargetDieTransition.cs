using System;
using UnityEngine;

namespace Scriptes.Creatures.StateMachine.Transitions
{
    public class TargetDieTransition : Transition
    {
        private void Update()
        {
            if (Target == null)
            {
                NeedTransit = true;
            }
        }
    }
}