using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterCube : MonoBehaviour
{
    [SerializeField] protected List<MonoBlock.BlockType> Deck = new List<MonoBlock.BlockType>();
    [SerializeField] protected int PlayerId = 0;

    protected Rigidbody RigidBody;
    protected List<MonoBlock> ChildBlocks = new List<MonoBlock>();
    protected bool PowerOff = false;

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
        Vector3 Center = this.transform.position;
        foreach (var b in Deck)
        {
            var block = MonoBlock.Build(b, PlayerId, x, y, z);
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
        }

        MonoBlockCache.SetPlayerDeck(PlayerId, ChildBlocks);
    }

    private void Update()
    {
        if (!RigidBody) return;

        if (!IsAction) return;

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

        //移動
        if (Input.GetMouseButtonDown(1))
        {
            IsAction = true;
        }

        if (!IsMove) return;

        Timer += Time.deltaTime;
        if (Timer > 1.0f)
        {
            Timer = 1.0f;
            IsMove = false;
        }

        this.transform.position = Vector3.Lerp(FromPos, TargetPos, Timer);
    }

    public void CallEvent()
    {

    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.CompareTag("MonoBlock"))
        {
            //ブロック取得
        }
    }
}
