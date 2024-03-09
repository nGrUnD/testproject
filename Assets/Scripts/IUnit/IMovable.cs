using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Move(Transform transform);
    void Rotate(Transform transform);
}
