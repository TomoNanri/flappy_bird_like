using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawn : MonoBehaviour
{
    public GameObject debrisPrefab;
    public int debrisPoolSize = 7;          
    public float debrisRate = 4.0f;         //デブリ再配置周期
    public float minDebrisRate = 0.05f;     //デブリ再配置の最短周期
    public float deltaDebrisRate = 0.01f;   //デブリ再配置周期の加速
    public float debrisXoffset = 5.0f;
    public float debrisYoffset = 7.0f;
    public float minRotateSpeed = -90f;     //最小（左）角速度
    public float maxRotateSpeed = 90f;      //最大（右）角速度
    public float spinRate = 20;             //角速度拡大係数


    GameObject[] debris;
    int currentDebris = 0;

    // Start is called before the first frame update
    void Start()
    {
        debris = new GameObject[debrisPoolSize];
        for (int i=0; i<debrisPoolSize; i++)
        {
            debris[i] = (GameObject)Instantiate(debrisPrefab, new Vector3(-6f, 10.0f, 0), Quaternion.identity);
        }
        StartCoroutine("DebrisLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    IEnumerator DebrisLoop()
    {
        while (true)
        {
            Vector3 pos = this.transform.position;
            Debug.Log("transform.position:" + pos);
            Vector3 birdPos = GameObject.Find("Bird").transform.position;
            pos.x = birdPos.x + debrisXoffset;              //Birdの進行方向へデブリの投下位置を設定する
            pos.y = debrisYoffset;                          //デブリの投下高度を変更する（難易度調整の意味合いあり）
            debris[currentDebris].transform.position = pos; //デブリ投下

            //落下物にスピン(degree/s)を与える（アセットのAddTorqueImpulseを呼ぶ）
            debris[currentDebris].GetComponent<DebrisScript>().AddTorqueImpulse(Random.Range(minRotateSpeed, maxRotateSpeed)*spinRate);

            if (++currentDebris >= debrisPoolSize)
            {
                currentDebris = 0;
                debrisRate = debrisRate<=minDebrisRate? minDebrisRate:debrisRate-deltaDebrisRate;
            }

            yield return new WaitForSeconds(debrisRate);
        }
    }
}
