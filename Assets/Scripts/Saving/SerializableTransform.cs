using System;
using UnityEngine;

namespace Puzzle.Saving
{
    [System.Serializable]
    public class SerializableTransform
    {
        float px, py, pz;
        float rx, ry, rz, rw;

        public SerializableTransform(Transform transform)
        {
            px = transform.position.x;
            py = transform.position.y;
            pz = transform.position.z;

            rx = transform.rotation.x;
            ry = transform.rotation.y;
            rz = transform.rotation.z;
            rw = transform.rotation.w;
        }

        public (Vector3 Position, Quaternion Rotation) ToTransform()
        {
            return new (new Vector3(px, py, pz), new Quaternion(rx, ry, rz, rw));
        }
    }
}