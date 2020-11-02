﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

namespace EnemyAction
{
    /// <summary>
    /// 弾をとばす
    /// </summary>
    class Bullet : ICommand
    {
        [SerializeField]
        int ShotNum = 1;

        [SerializeField]
        int Atk = 1;

        [SerializeField]
        float Interval = 0;

        [SerializeField]
        string BulletName = "PhysicsBall";

        MasterCube Owner;
        //LinkedList<Summon.Bullet> Summon = new LinkedList<Summon.Bullet>();
        float Timer = 0.0f;
        int ShotCount = 0;

        public void Setup(MasterCube owner)
        {
            Owner = owner;
            Timer = 0.0f;
            ShotCount = 0;
        }

        public bool Execute()
        {
            Timer += Time.deltaTime;
            if (Timer < Interval) return false;

            while(ShotCount < ShotNum)
            {
                var blt = BulletObject.Bullet.Build<BulletObject.PhysicsBall>(BulletName, Owner, null, null);
                blt.transform.position = Owner.transform.position + Owner.transform.forward * 1.5f;
                blt.AddForce(Owner.transform.forward * 250.0f);

                ShotCount++;
                Timer = 0.0f;
                if (Interval > 0.0f) break;
            }
            return (ShotCount >= ShotNum);
        }
    }
}
