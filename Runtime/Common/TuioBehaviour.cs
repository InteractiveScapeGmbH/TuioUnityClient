using System;
using UnityEngine;

namespace TuioUnity.Common
{
    public abstract class TuioBehaviour : MonoBehaviour
    {
        public abstract uint SessionId { get; protected set; }
        public abstract uint Id { get; protected set; }
        public abstract event Action OnUpdate;
    }
}