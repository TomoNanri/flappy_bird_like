using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        AddTorqueImpulse(rotateSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // start rotation
    public void AddTorqueImpulse(float angularChangeInDegrees)
    {
        var body = GetComponent<Rigidbody2D>();
        var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * body.inertia;  //ラジアン/s＊慣性モーメント

        body.AddTorque(impulse, ForceMode2D.Impulse);       //impulseなので瞬時に指定速度になる
    }
}
