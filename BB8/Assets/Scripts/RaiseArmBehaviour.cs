using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseArmBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       MovementLogic m_movementLogic = GameObject.FindGameObjectWithTag("Body").GetComponent<MovementLogic>();
        
        if(m_movementLogic) {
           if(m_movementLogic.m_vfx == VFXtoPlay.BlowTorch) {
               m_movementLogic.PlayBlowTorch();
           } else if (m_movementLogic.m_vfx == VFXtoPlay.ElectricArk) {
               m_movementLogic.PlayElectricArk();
           }
       }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //    
    // }

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
