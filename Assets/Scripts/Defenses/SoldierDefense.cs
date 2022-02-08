using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoldierDefense : BaseDefense
{
    [Header("References")]
    [SerializeField] CircleCollider2D _attackHitbox;

    [Header("Settings")]
    [SerializeField] [Range(0.5f, 3)] float _hitTime = 1;
    [SerializeField] [Range(0.5f, 3)] float _idleTime = 1;

    FSMachine _machine;

    const string AnimatorHitBool = "IsHitting";
    const string AnimatorImprovedBool = "IsImproved";

    bool _isImproved;

    float _hitElapsedTime;
    float _idleElapsedTime;



    private void Awake()
    {
        Initialize();
        type = RulerType.Type2;
        _animator = GetComponent<Animator>();

        CreateMachine();
    }

    private void Update()
    {
        _machine.Update();
    }

    override protected void Die()
    {
        StartCoroutine(Shake.DOShake(.15f, .3f, FindObjectOfType<Camera>().transform));
        Destroy(gameObject);
    }

    public override void Improve()
    {
        _isImproved = true;

        _animator.SetBool(AnimatorImprovedBool, true);
    }

    void CreateMachine()
    {
        FSMState idleState = new FSMState("Idle",
            () =>
            {
                var fixedTime = _hitTime;
                if (_isImproved) _hitTime *= 2;

                if (_hitElapsedTime > fixedTime)
                {
                    _idleElapsedTime = 0;
                    _animator.SetBool(AnimatorHitBool, false);
                    _attackHitbox.enabled = false;
                    return true;
                }

                return false;
            }, () =>
            {
                _idleElapsedTime += Time.deltaTime;
            });


        FSMState hitState = new FSMState("Hit",
            () =>
            {

                if (_idleElapsedTime > _idleTime)
                {
                    _hitElapsedTime = 0;
                    _animator.SetBool(AnimatorHitBool, true);
                    _attackHitbox.enabled = true;
                    return true;

                }

                return false;

            },

            () =>
            {
                _hitElapsedTime += Time.deltaTime;
            });

        idleState.children.Add(hitState);
        hitState.children.Add(idleState);
        _machine = new FSMachine(idleState);



    }
}
