using UnityEngine;
using System.Collections;

public class Bomb : MonoBlock
{
    [SerializeField] float BreakTime = 1.0f;
    [SerializeField] float Size = 1.0f;
    [SerializeField] Attacker BombCircle = null;

    bool IsIgnite = false;
    float Timer = 0.0f;

    protected override void Setup()
    {
        BombCircle.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (!IsIgnite) return;
        Timer += Time.deltaTime;
        if(Timer >= BreakTime)
        {
            BombCircle.transform.localScale = new Vector3(Size, Size, Size);
            Life = 0;
        }
    }

    public void BreakShot()
    {
        IsIgnite = true;
    }

    public override void Damage(int dmg)
    {
        base.Damage(dmg);
        IsIgnite = true;
        Timer = BreakTime;
    }
}
