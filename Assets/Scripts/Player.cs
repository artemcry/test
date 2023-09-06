using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class Player : MonoBehaviour
{
    
    public float speed = 5.0f;
    public float rotationSpeed = 10;
 
    private Animator animator;
    private Rigidbody rb;
    public FloatingJoystick joystick;

    [SerializeField]
    AudioClip[] stepAudioClips;
    [SerializeField]
    AudioClip treeDamageAudio;
    [SerializeField]
    AudioClip takeDropAudio;
    private TreeController treeProcessed;
    [SerializeField]
    private RightHandTool rightHandTool;
    [SerializeField]
    private TextMeshProUGUI woodCountTMP;
    private int woodCount;
    AudioSource audioSource;

    void Start()
    {
        treeProcessed = null;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        woodCount = 0;  
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {      
        float v = joystick.Horizontal;
        float h = joystick.Vertical;
        var dirvec = new Vector3(v, 0, h);
        if (Mathf.Abs(dirvec.magnitude) > Mathf.Abs(0.05f))
           transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirvec), rotationSpeed);
        animator.SetFloat("run_speed", Vector3.ClampMagnitude(dirvec, 1).magnitude);
        rb.velocity = Vector3.ClampMagnitude(dirvec, 1) * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "tree")
        {
            rightHandTool.setCurrentTool("Axe");
            treeProcessed = collision.gameObject.GetComponent<TreeController>();
            animator.SetBool("tree_attack", true);                        
        } else if(collision.gameObject.tag == "tree piece")
        {
            Destroy(collision.gameObject);
            woodCount++;
            woodCountTMP.text = woodCount.ToString();
            audioSource.PlayOneShot(takeDropAudio);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (treeProcessed) {
            stopAttackTree();
        }
    }
    public void stopAttackTree() {
        rightHandTool.setCurrentTool(null);
        animator.SetBool("tree_attack", false);
    }
    public void stepSound()
    {
        audioSource.PlayOneShot(stepAudioClips[Random.Range(0, stepAudioClips.Length-1)]);
    }
    public void treeDamageSound()
    {
        audioSource.PlayOneShot(treeDamageAudio);
        if (treeProcessed.GetDamage(25))
            stopAttackTree();

    }
}
