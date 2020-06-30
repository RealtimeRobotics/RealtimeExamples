#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
import sys
import time

def startup_sequence(cmdr,project_info,group):
    '''
    This function gets all projects in the provided group
    For each group, InitGroup is called
    If all groups were initialized successfully, BeginOperationMode is called
    '''
    code, data = cmdr.GetMode()

    for project_name,info in project_info.items():
        workstate = info['workstates'][0]
        cmdr.InitGroup(workstate,group_name = group,project_name=project_name)
    
    return cmdr.BeginOperation()

def shutdown(cmdr,group,unload=True):
    '''
    This function cancels all current moves and puts the controller back in config mode
    Optionally: you can have the function wait until all moves are finished to cancel
    '''
    # End operation mode on controller. This will put the controller in config mode
    cmdr.EndOperation()

    # This will unload the group from the controller
    if unload:
        cmdr.TerminateGroup(group)

def put_on_roadmap(cmdr,project_info,group,hub='home'):
    '''
    This function puts the robot in the procided project back on the roadmap
    The nearest hub is chosen
    '''
    code, data = cmdr.GetMode()
    if data != "OPERATION":
        print('Put controller in operation mode!')
        return

    move_res = []
    for project_name,info in project_info.items():
        workstate = info['workstates'][0]
        cmdr.InitGroup(workstate,group_name = group,project_name=project_name)
        hub_res, hub_seq = cmdr.OffroadToHub(workstate, hub, "low", 30.0, True, project_name)
        move_res.append(cmdr.WaitForMove(hub_seq))

    return move_res
    

def attempt_fault_recovery(cmdr,project_info,group,hub='home'):
    '''
    This function clears faults for the provided project, and puts the robot back on the roadmap
    '''

    # Clear faults on the RTR Controller
    cmdr.ClearFaults()

    # For each 
    put_on_roadmap(cmdr,project_info,group,hub)
