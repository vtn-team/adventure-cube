using UnityEngine;
using System.Collections;

public class Laser : Attacker
{
    public float ShotSpan = 1.0f;

    float Timer = 0.0f;
    bool ShotFlag = false;

    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= ShotSpan)
        {
            Timer -= ShotSpan;
            ShotFlag = true;
        }

        if(ShotFlag)
        {
            float dist = 50.0f;
            RaycastHit[] hits = Physics.RaycastAll(this.transform.position + this.transform.forward, this.transform.forward, 100.0f);
            foreach(var h in hits)
            {
                if (!h.collider.gameObject.CompareTag("MonoBlock")) continue;

                var tgt = MonoBlockCache.GetCache(h.collider.gameObject);
                if(tgt)
                {
                    tgt.Damage(Atk);
                    dist = h.distance;
                }
            }

            this.transform.localScale = new Vector3(0.5f, 0.5f, dist);
            this.transform.localPosition = new Vector3(0, 0, dist / 2 + 1);
        }
    }
}
