//////////////////////////////////////////////////////
// MK Toon Examples RotateObject                	//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2020 All rights reserved.            //
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK.Toon.Examples
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField]
        private float _rotateSpeedY = 1;
        [SerializeField]
        private float _bounceSpeed = 1;
        [SerializeField]
        private float _bounceLimit = 0.05f;

        private Vector3 _startPos = Vector3.zero;

        private void Start()
        {
            _startPos = transform.position;
        }

        private void Update()
        {
            Vector3 pos = new Vector3(transform.position.x, _startPos.y, transform.position.z);
            pos.y += Mathf.Sin(Time.time * _bounceSpeed) * _bounceLimit;
            transform.position = pos;

            transform.Rotate(new Vector3(0, _rotateSpeedY, 0) * Time.smoothDeltaTime, Space.World);
        }
    }
}
