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
        GameObject Target;

        [SerializeField]
        float Speed = 1.0f;

        [SerializeField]
        float Range = 5.0f;

        float Timer = 0.0f;
        float TotalTime;
        Vector3 StartPos;
        Vector3 TargetPos;

        public void Setup(GameObject owner)
        {
            Target = owner;
            Timer = 0.0f;

            StartPos = Target.transform.position;
            TargetPos = StartPos + (Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0) * new Vector3(UnityEngine.Random.Range(0, Range), 0, 0));
            TotalTime = (TargetPos - StartPos).magnitude / Speed;
        }

        public bool Execute()
        {
            //Target.transform.position = Vector3.Slerp(StartPos, TargetPos, Timer / TotalTime);
            Target.transform.position += (TargetPos - Target.transform.position) * Time.deltaTime;
            return (Target.transform.position - TargetPos).sqrMagnitude < 5.0f;
        }
    }
}