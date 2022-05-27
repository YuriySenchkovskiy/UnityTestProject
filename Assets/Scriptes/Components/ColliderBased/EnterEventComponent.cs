using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptes.Components.ColliderBased
{
    [Serializable]
    public class EnterEventComponent : UnityEvent<GameObject>
    {
    }
}