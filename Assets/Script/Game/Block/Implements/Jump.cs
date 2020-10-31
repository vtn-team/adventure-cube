using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Block;

//namespace Block
//{
    public class Jump : MonoBlock, IPassiveBlock
    {
        public PassiveType PassiveType => PassiveType.Jump;      // タイプを返す実装が必要

        float y;

        bool on = true;

        // パッシブ効果を処理する実装が必要
        public int PassiveEvent(int param, int subparam)
        {
            return figure;
        }

        void Update()
        {
            
        if (on)
        {
            Up();
        }
        else
        {
            Down();
        }
   
        }

    void Up()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        pos.y += 0.5f;

        myTransform.position = pos;

        if (pos.y >= 30)
        {
            on = false;
        }
    }

    void Down()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        pos.y -= 0.5f;

        myTransform.position = pos;

        if (pos.y <= 5f)
        {
            on = true;
        }
    }
    }
//}

