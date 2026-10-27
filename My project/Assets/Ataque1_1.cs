using UnityEngine;

public class Ataque1_1 : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    [SerializeField] int state, projetil;
    private ProjectilePool bossPool => GameObject.Find("BossPool").GetComponent<ProjectilePool>();
    private Transform boss;
    private Vector3 pontoDeDisparo;

    private float intervalo = 4.5f;
    private float count;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.gameObject.transform;
        bossPool.UpdateData(state, projetil);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(count < intervalo)
        {
            count += Time.deltaTime;
        }
        else
        {
            float largura = boss.lossyScale.x / 2f;
            float altura = boss.lossyScale.y / 2f;

            pontoDeDisparo = new Vector3(Random.Range(boss.position.x - largura, boss.position.x + largura), boss.position.y - altura, boss.position.z);
            boss.gameObject.GetComponent<BossShooter>().Atirar(pontoDeDisparo);
            count = 0;

            animator.SetBool("Ataque1", false);
            animator.SetBool("Ataque2", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    
}
