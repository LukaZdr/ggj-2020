using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusText : MonoBehaviour
{
    [SerializeField]
    TextMesh textMesh;
    [SerializeField]
    [Multiline]
    string text = "{0:d2}:{1:d2}\n\n{2:d} Units of Coal burned\n{3:d} Liters of Water leaked";

    bool _lost = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.lost)
        {
            int time = (int)Time.time;
            int min = time / 60;
            int sec = time % 60;
            int coal = (int)GameManager.instance.coalBurned;
            int water = (int)GameManager.instance.leakage;
            textMesh.text = string.Format(text, min, sec, coal, water);
        }else if (!_lost)
        {
            textMesh.text += "\n\nGame Over";
            _lost = true;
        }
    }
}
