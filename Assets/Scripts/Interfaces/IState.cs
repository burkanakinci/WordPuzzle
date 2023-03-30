using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IState
{
    void Enter();
    void LogicalUpdate();
    void PhysicalUpdate();
    void Exit();
}