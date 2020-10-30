using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    [SerializeField] AnimationCurve AnimCurve = null;

    Text Text;
    float time = 0.0f;
    Vector3 MovVec = new Vector3(0, 0.2f, 0);
    Vector3 Mov = Vector3.zero;
    Vector2 Random = Vector2.zero;
    RectTransform Transform;
    GameObject Target;

    private void Awake()
    {
        Transform = GetComponent<RectTransform>();
        Text = GetComponent<Text>();
        Random = new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        Mov += MovVec* AnimCurve.Evaluate(time);
        Transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position) + Random;
        Transform.position += Mov;

        if (time > 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Set(GameObject go, int dmg)
    {
        Target = go;
        Text.text = dmg.ToString();
    }

    public void SetColor(Color col)
    {
        Text.color = col;
    }
}
