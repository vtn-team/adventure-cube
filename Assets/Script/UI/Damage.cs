using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    [SerializeField] AnimationCurve AnimCurve = null;

    Text Text;
    float time = 0.0f;
    Vector3 Mov = new Vector3(0, 1, 0);
    RectTransform Transform;

    private void Awake()
    {
        Transform = GetComponent<RectTransform>();
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Transform.position += Mov * AnimCurve.Evaluate(time);
        if(time > 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Set(int dmg)
    {
        Text.text = dmg.ToString();
    }
}
