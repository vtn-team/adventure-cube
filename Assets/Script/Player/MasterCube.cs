using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;
using Summon;

public class MasterCube : MonoBehaviour
{
    [SerializeField] protected List<MonoBlock.BlockType> Deck = new List<MonoBlock.BlockType>();
    [SerializeField] protected int PlayerId = 0;

    protected Rigidbody RigidBody;
    protected List<MonoBlock> ChildBlocks = new List<MonoBlock>();
    protected bool PowerOff = false;
    protected ISummonGroup[] SummonGroup = new ISummonGroup[(int)SummonObject.SummonType.MAX];

    float Timer = 0.0f;
    Vector3 FromPos;
    Vector3 TargetPos;
    bool IsMove = false;
    bool IsAction = false;

    public void Build()
    {
        RigidBody = GetComponent<Rigidbody>();
        /*
        var deck = GameManager.GetDeck(PlayerId);
        if (deck == null)
        {
            deck = Deck;
        }
        */

        int x = -1;
        int y = -1;
        int z = -1;
        int index = 1;
        Vector3 Center = this.transform.position;
        foreach (var b in Deck)
        {
            var block = MonoBlock.Build(b, index, PlayerId, x, y, z, this);
            ChildBlocks.Add(block);
            block.transform.parent = this.transform;
            block.transform.position = new Vector3(Center.x + x, Center.y + y + 2.0f, Center.z + z);

            x++;
            if (x > 1)
            {
                x = -1;
                y++;
                if(y > 1)
                {
                    y = -1;
                    z++;
                    if (z > 1) break;
                }
            }
            index++;
        }

        MonoBlockCache.SetPlayerDeck(PlayerId, ChildBlocks);
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

    private void OnTriggerEnter(Collider target)
    {
        if(target.CompareTag("MonoBlock"))
        {
            //ブロック取得
        }
    }
}
