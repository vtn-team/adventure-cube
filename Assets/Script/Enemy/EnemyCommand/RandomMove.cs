using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyAction
{
    [Serializable]
    public class RandomMove : ICommand
    {
        MasterCube Owner;

        [SerializeField]
        float Speed = 1.0f;

        [SerializeField]
        float Range = 5.0f;

        float Timer = 0.0f;
        float TotalTime;
        Vector3 StartPos;
        Vector3 TargetPos;

        public void Setup(MasterCube owner)
        {
            Owner = owner;
            Timer = 0.0f;

            StartPos = Owner.transform.position;
            TargetPos = StartPos + (Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0) * new Vector3(UnityEngine.Random.Range(0, Range), 0, 0));
            TotalTime = (TargetPos - StartPos).magnitude / Speed;
            Owner.transform.LookAt(TargetPos, Vector3.up);
        }

        public bool Execute()
        {
            //Target.transform.position = Vector3.Slerp(StartPos, TargetPos, Timer / TotalTime);
            Owner.transform.position += (TargetPos - Owner.transform.position) * Time.deltaTime;
            return (Owner.transform.position - TargetPos).sqrMagnitude < 5.0f;
        }
    }
}