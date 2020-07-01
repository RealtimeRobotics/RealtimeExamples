#!/usr/bin/env python3

from PythonCommander import PythonCommander
import sys
import time

def startup_sequence(cmdr,project_info,group):
    '''
    This function calls InitGroup for each project in a group.
    If all InitGroup calls succeed, the Controller is placed in run mode

    Parameters:
        cmdr (object): Instance of the PythonCommander
        project_info (nested dict): Nested dict returned by PythonCommanderHelper's get_project_info() method
        group (string): Group being controlled
    
    Returns:
        startup_responses: A dictionary with keys: InitGroupResponses and BeginOperationResponse and the values are the response codes
    '''
    startup_responses = {'InitGroupResponses':[],'BeginOperationResponse':[]}
    init_responses = []
    for project_name,info in project_info.items():
        workstate = info['workstates'][0]
        resp = cmdr.InitGroup(workstate,group_name = group,project_name=project_name)
        init_responses.append(int(resp))
    startup_responses['InitGroupResponses'] = init_responses

    # If all InitGroup calls returned 0, begin operation mode
    if sum(init_responses) == 0:
        begin_resp = cmdr.BeginOperation()
        startup_responses['BeginOperationMode'] = begin_resp
    else:
        print('Could not initialize all projects!')
    
    return startup_responses

def shutdown(cmdr,group,unload=True):
    '''
    This function takes the controller out of Operation mode and into Config mode.
    Optionally, the group can be unloaded from the control panel

    Parameters:
        cmdr (object): Instance of the PythonCommander
        group (string): Group being controlled
        unload (string): If true, the group will be unloaded from the Control Panel
    '''
    # End operation mode on controller. This will put the controller in config mode
    cmdr.EndOperation()

    # This will unload the group from the controller
    if unload:
        cmdr.TerminateGroup(group)

def put_on_roadmap(cmdr,project_info,group,hub='home'):
    '''
    This function puts all robots back on the roadmap

    Parameters:
        cmdr (object): Instance of the PythonCommander
        project_info (nested dict): Nested dict returned by PythonCommanderHelper's get_project_info() method
        group (string): Group being controlled
        hub (string): Hub to move the robots to. Default is 'home'
    
    Returns:
        move_res (list): a list of the move result value from each offroad to hub call. 0 means success
    '''
    code, data = cmdr.GetMode()
    if data != 'OPERATION':
        print('Controller not in operation mode. Calling startup_sequence()')
        return

    move_res = []
    for project_name,info in project_info.items():
        workstate = info['workstates'][0]
        cmdr.InitGroup(workstate,group_name = group,project_name=project_name)
        hub_res, hub_seq = cmdr.OffroadToHub(workstate, hub, "low", 120.0, True, project_name)
        move_res.append(cmdr.WaitForMove(hub_seq))

    return move_res
    

def attempt_fault_recovery(cmdr,project_info,group,hub='home'):
    '''
    This function clears faults on the controller and puts all robots back on the roadmap

    Parameters:
        cmdr (object): Instance of the PythonCommander
        project_info (nested dict): Nested dict returned by PythonCommanderHelper's get_project_info() method
        group (string): Group being controlled
        hub (string): Hub to move the robots to. Default is 'home'
    '''

    # Clear faults on the RTR Controller
    cmdr.ClearFaults()

    # Restart controller
    startup_sequence(cmdr,project_info,group)

    # Put each robot on the roadmap
    put_on_roadmap(cmdr,project_info,group,hub)
