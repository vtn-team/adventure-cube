using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;
using Summon;

public class Player : MasterCube
{
    float Timer = 0.0f;
    Vector3 FromPos;
    Vector3 TargetPos;
    bool IsMove = false;
    bool IsAction = false;

    public override void Build()
    {
        base.Build();

        LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 1);
    }

    protected override void Death()
    {
        //tbd
        //とりあえず
    }

    void UnityUpdate()
    {
        //if (!RigidBody) return;

        if (!IsMove)
        {
            //移動
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool is_hit = Physics.Raycast(ray, out hit);
                if (is_hit)
                {
                    Timer = 0.0f;
                    FromPos = this.transform.position;
                    TargetPos = hit.point;
                    TargetPos.y = 0.0f;
                    IsMove = true;
                    this.transform.LookAt(TargetPos, Vector3.up);
                }
            }
        }

        if (!IsAction)
        {
            //移動
            if (Input.GetMouseButtonDown(1))
            {
                IsAction = true;
            }
        }

        if (IsMove)
        {
            Timer += Time.deltaTime;
            if (Timer > 1.0f)
            {
                Timer = 1.0f;
                IsMove = false;
            }
            this.transform.position = Vector3.Lerp(FromPos, TargetPos, Timer);
        }

        /*
        //キューブの処理
        ChildBlocks.ForEach(c => c.UpdateBlock());

        //サモナーの処理
        for (int i = 0; i < (int)SummonObject.SummonType.MAX; ++i)
        {
            if (SummonGroup[i] == null) continue;
            SummonGroup[i].Update();
        }
        */
    }
}

