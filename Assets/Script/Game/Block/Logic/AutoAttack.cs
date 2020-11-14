using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Block
{
    public class AttackTimer
    {
        public float Current => (Timer / Interval);
        public bool IsAttackOK => (Timer >= Interval);
        float Timer = 0;
        float Interval;

        public void Setup(float interval)
        {
            Timer = 0;
            Interval = interval;
        }

        public void Update()
        {
            Timer += Time.deltaTime;
        }

        public void ResetTimer(bool isIntervalReset = true)
        {
            if (isIntervalReset)
            {
                Timer -= Interval;
            }
            else
            {
                Timer = 0;
            }
        }
    }
}
