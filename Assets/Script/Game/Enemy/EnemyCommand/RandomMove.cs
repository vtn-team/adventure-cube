using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyAction
{
    /// <summary>
    /// ランダム徘徊
    /// </summary>
    [Serializable]
    public class RandomMove : ICommand
    {
        MasterCube Owner;

        [SerializeField]
        float Speed = 1f;

        [SerializeField]
        float Range = 10.0f;

        float Timer = 0.0f;
        float TotalTime;
        Vector3 StartPos;
        Vector3 TargetPos;

        public void Setup(MasterCube owner)
        {
            Owner = owner;
            Timer = 0.0f;
            if (owner.SpeedCubes != null)
            {
                for (int i = 1; owner.SpeedCubes.Count >= i; i++)
                {
                    Speed--;
                }
            }
            
            StartPos = Owner.transform.position;
            TargetPos = StartPos + (Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0) * new Vector3(UnityEngine.Random.Range(0, Range), 0, 0));
            TotalTime = (TargetPos - StartPos).magnitude / Speed;
            Owner.transform.LookAt(TargetPos, Vector3.up);
        }

        public bool Execute()
        {
            //Target.transform.position = Vector3.Slerp(StartPos, TargetPos, Timer / TotalTime);
            Vector3 sub = TargetPos - Owner.transform.position;
            sub.y = 0.0f;
            Owner.transform.position += sub * Time.deltaTime;
            return (Owner.transform.position - TargetPos).sqrMagnitude < 5.0f;
        }
    }
}