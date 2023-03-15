using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    Collider2D[] enemyColl;
    Collider2D[] bulletColl;
    GameObject area;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        area = GameObject.FindWithTag("Area");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        
        enemyColl = GameManager.instance.pool.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < enemyColl.Length; i++) {
            Physics2D.IgnoreCollision(area.GetComponent<Collider2D>(), enemyColl[i]);
        }

    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == transform.gameObject)
        {
            // �θ� ������Ʈ�� �浹�� ���, �浹 �̺�Ʈ ó��
            Debug.Log("�θ� ������Ʈ�� �浹�߽��ϴ�.");
        }
        else
        {
            // �ڽ� ������Ʈ�� �浹�� ���, ����
            return;
        }
        if (!collision.CompareTag("Enemy")) return;

        GameManager.instance.health -= 5;

        if (GameManager.instance.health < 0)
        {
            anim.SetBool("Dead", true);
        }
    }
}
