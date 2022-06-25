using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.ColliderBased
{
    [Serializable]
    public class EnterEventComponent : UnityEvent<GameObject>
    {
    }
}