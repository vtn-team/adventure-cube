using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;
using Summon;

public class MasterCube : MonoBehaviour
{
    protected Rigidbody RigidBody;
    protected List<MonoBlock> ChildBlocks = new List<MonoBlock>();
    protected bool PowerOff = false;
    protected IObjectGroup<SummonObject>[] SummonGroup = new IObjectGroup<SummonObject>[(int)SummonObject.SummonType.MAX];

    public DamageCaster TakeDamageCaster { get; private set; }
    public DamageCaster AttackDamageCaster { get; private set; }

    float Timer = 0.0f;
    Vector3 FromPos;
    Vector3 TargetPos;
    bool IsMove = false;
    bool IsAction = false;

    public virtual void Build()
    {
        RigidBody = GetComponent<Rigidbody>();
        
        TakeDamageCaster = new DamageCaster(ChildBlocks);
        AttackDamageCaster = new DamageCaster(ChildBlocks);
    }

    private void Update()
    {
        if (!RigidBody) return;

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


        //キューブの処理
        ChildBlocks.ForEach(c => c.UpdateBlock());

        //サモナーの処理
        for(int i=0; i<(int)SummonObject.SummonType.MAX; ++i)
        {
            if (SummonGroup[i] == null) continue;
            SummonGroup[i].Update();
        }
    }

    public void CallEvent()
    {

    }


    public void AddSummonGroup(SummonObject.SummonType type, SummonObject summon)
    {
        if(SummonGroup[(int)type] == null)
        {
            SummonGroup[(int)type] = SummonObject.Build(type);
        }
        SummonGroup[(int)type].Add(summon);
        SummonGroup[(int)type].Replace();
    }

    public void Damage(int dir, int dmg)
    {
        int TargetId = 0;
        TargetId = TakeDamageCaster.CastDamage(dir, ref dmg);
        if (TargetId == -1) return;

        Random.Range(0,26);

    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.CompareTag("MonoBlock"))
        {
            //ブロック取得
        }
    }
}
