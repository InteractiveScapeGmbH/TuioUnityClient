using UnityEngine;

namespace TuioUnity.Common
{
    /// <summary>
    /// Describes which tuio specification should be used.
    /// </summary>
    public enum TuioVersion
    {
        [InspectorName("Tuio 1.1")]
        Tuio11,
        [InspectorName("Tuio 2.0")]
        Tuio20
    }
}