using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatMachineState : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerCombatStateFactory factory;
    private PlayerCombatBaseState currentState;
    private Animator anim;


    //attacking variables
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool cancelComboCountdown = false;
    private bool comboCounting = false;
    private float MAXCOMBOCOOLDOWNTIMER = 3f;
    private float comboTimer = 3f;


    //spear throwing
    public GameObject spearObject;
    private bool haveSpear = true;
    private float MAXTHROWCOOLDOWN = 3f;
    [SerializeField]
    private Spear currentSpear;
    private float force = 1000.0f;
    [SerializeField]
    private Transform handTran;
    private bool canThrowSpear;

    //test attack variables

    public Material defaultmat;
    public Material attackMat;
    public MeshRenderer weaponMeshRender;

    public PlayerControls PlayerControls { get => playerControls; }
    public PlayerCombatBaseState CurrentState { get => currentState; set => currentState = value; }
    public Animator Anim { get => anim; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public Spear CurrentSpear { get => currentSpear; }
    public float Force { get => force; set => force = value; }
    public bool HaveSpear { get => haveSpear; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public bool CanThrowSpear { get => canThrowSpear; set => canThrowSpear = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerControls>();

        factory = new PlayerCombatStateFactory(this);
        currentState = factory.Idle();
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();

        AttackComboCountdown();
    }

    public void StartThrowCooldown()
    {
        StartCoroutine(ThrowCooldown());
    }

    private IEnumerator ThrowCooldown()
    {
        haveSpear = false;
        yield return new WaitForSeconds(MAXTHROWCOOLDOWN);

        GameObject newSpear = Instantiate(spearObject, handTran);
        currentSpear = newSpear.GetComponent<Spear>();
        weaponMeshRender = newSpear.GetComponent<MeshRenderer>();
        haveSpear = true;
    }

    public void StartComboCountdown()
    {
        //start combo countdown (restart it if it has already started)
        comboCounting = true;
        comboTimer = MAXCOMBOCOOLDOWNTIMER;
        cancelComboCountdown = false;
    }

    public void StopComboCountdown()
    {
        comboCounting = false;
    }

    private void AttackComboCountdown()
    {
        if (!comboCounting) return;

        if (cancelComboCountdown)
        {
            cancelComboCountdown = false;
            comboCounting = false;
            return;
        }

        comboTimer -= Time.deltaTime;

        if (comboTimer <= 0)
        {
            anim.SetInteger("Counter", 0);
            comboCounting = false;
        }
    }

    public void ThrowSpear()
    {
        // fix the issue of the spear not being thrown straight;
        //TODO: animation that throws the spear correctly.
        Vector3 dir = transform.rotation.eulerAngles;
        Debug.Log(dir);
        dir.y = dir.y > 190 ? 0 : -180;
        currentSpear.Throw(force, Quaternion.Euler(dir));
        StartThrowCooldown();
    }
}
