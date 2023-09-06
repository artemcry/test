using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TreeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private const int maxHP = 100;
    private Slider hpUI;
   
    private GameObject canvas;
    [SerializeField]
    private GameObject resource;
    [SerializeField]
    Player player;
    private int hp;
    protected int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if(hpUI != null)
                hpUI.value = (float)hp / maxHP;
            canvas?.SetActive(hp < maxHP);
        }
    }

    public bool GetDamage(int damage)
    {
        HP -= damage;
        if (HP <=0 )
        {
            GameObject g = Instantiate(resource);
            g.transform.position = transform.position + (transform.position - player.transform.position);

            float upwardForce = 2f; 

            Rigidbody rb = g.GetComponent<Rigidbody>();

            Vector3 randomDirection = Random.onUnitSphere.normalized;

            Vector3 forceDirection = new Vector3(randomDirection.x, 1f, randomDirection.z).normalized;
            rb.AddForce(forceDirection * upwardForce, ForceMode.Impulse);

            gameObject.SetActive(false);
            Invoke("ActivateObject", 15f);

            return true;         
        }
        return false;
    }

    void ActivateObject()
    {
        gameObject.SetActive(true);
        HP = maxHP;
    }

    void Start()
    {
        HP = maxHP;
        canvas =  transform.Find("Hp level").gameObject;
        hpUI = transform.Find("Hp level/Panel/Slider").gameObject.GetComponent<Slider>();
    }




}
