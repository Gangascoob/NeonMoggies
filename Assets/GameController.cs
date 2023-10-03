using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI ScoreText;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreUp()
    {
        _score++;
        ScoreText.text = _score.ToString();
    }
}
