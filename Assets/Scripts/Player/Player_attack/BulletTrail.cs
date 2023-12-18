using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;

    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    // Update is called once per frame
    void Update()
    {
        _progress += _speed * Time.deltaTime;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition.WithAxis(Axis.Z, -1);
    }
}

public static class VectorExtension
{
    public static Vector3 WithAxis(this Vector3 vector, Axis axis, float value)
    {
        return new Vector3(
            x:axis == Axis.X ? value: vector.x,
            y:axis == Axis.Y ? value : vector.y,
            z: axis == Axis.Z ? value : vector.z
            );
    }
}

public enum Axis
{
    X, Y, Z
}
