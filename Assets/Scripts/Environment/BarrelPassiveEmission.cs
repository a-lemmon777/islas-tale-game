using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPassiveEmission : StateMachineBehaviour
{
    private Coroutine _emission;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var barrel = animator.GetComponent<ToxicBarrel>();

        /// <summary>
        /// Periodically emits toxic obstacles
        /// </summary>
        /// <param name="interval">Time interval in seconds</param>
        /// <returns></returns>
        IEnumerator PeriodicEmission(float interval)
        {
            while (true)
            {
                barrel.EmitToxicObstacle();
                yield return new WaitForSeconds(interval);
            }
        }

        this._emission = barrel.StartCoroutine(PeriodicEmission(barrel.TimeInterval));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ToxicBarrel>().StopCoroutine(_emission);
    }

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
