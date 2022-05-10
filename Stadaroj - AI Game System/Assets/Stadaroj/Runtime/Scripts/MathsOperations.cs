using UnityEngine;

namespace Maths
{
    public static class MathsOperations
    {
        /// The squared value of a number
        public static float Square(float value)
        {
            return value * value;
        }

        /// Subtract two vectors
        public static Vector3 Vector3Subtract(Vector3 v1, Vector3 v2)
        {
            return new Vector3( v1.x - v2.x, v1.y - v2.y, v1.z - v2.z );
        }

        /// Multiply vector by scalar
        public static Vector3 Vector3Scale(Vector3 v, float scalar)
        {
            return new Vector3( v.x * scalar, v.y * scalar, v.z * scalar );
        }
    }
}