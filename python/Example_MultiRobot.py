#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
from lib.PythonCommanderHelper import PythonCommanderHelper
import lib.CommonOperations as cmn_ops 
import time, random, sys, threading

def main():
    # Setup the PythonCommander which is responsible for sending commands to the 
    # RTR Controller that control the robot/s
    cmdr = PythonCommander("127.0.0.1", 9999) #TODO: pass IP as argument.
    cmdr.Reconnect()

    # Setup the PytonCommanderHelper which is responsible for getting data from
    # the RTR Controller through its REST api
    helper = PythonCommanderHelper("127.0.0.1") #TODO: pass IP as argument. :3000 is for running from source
    group_info = helper.get_group_info()

    # Put appliance in config mode so that we can load/unload
    resp = helper.put_config_mode()

    # Check if a project is already loaded
    for group_name,info in group_info.items():
        if info['loaded'] == True:
            loaded_group = group_name
            print('Group {%s} is loaded, would you like to unload it? (y/n) '%(group_name),end = '')
            user_in = input()
            break
    
    # Prompt the user if they would like to unload that group
    # You must unload the current group, if you will be switching groups
    try:
        if user_in == 'y': 
            helper.put_unload_group(group_name)
    except:
        pass

    # Print installed groups on the Controller
    print("\nAvailable Groups:")
    for count,group_name in enumerate(group_info):
        print('\t' + str(count) + ' : ' + str(group_name))
    
    # Select a deconfliction group to load
    group_selected = False
    while not group_selected:
        selected_group = input('\nWhat group would you like to control: ')
        try:
            selected_projects_info = group_info[selected_group]
            group_selected = True
        except:
            print('Group not found in current groups loaded in Control Panel')

    # Load the selected group
    resp = helper.put_load_group(selected_group)
    print('\nGroup loaded: %s'%(selected_group))
    print('Loaded projects: {}'.format(selected_projects_info['projects']))
    
    # Get the groups info
    project_info = helper.get_project_info(group_info[selected_group])
    # Call InitGroup for every project, and then BeginOperationMode
    code = cmn_ops.startup_sequence(cmdr,project_info,selected_group)

    # Check if the user is ready for robots to begin moving
    print("The robots are initialized and operation mode succeeded.")
    user_in = input('Can they begin moving? (y/n) ')
    if not user_in == 'y':
        sys.exit()

    # Call offroad to hub for every project
    # The assumption here is that there is a hub named 'home'! We consider that a best practice
    res = cmn_ops.put_on_roadmap(cmdr,project_info,selected_group,hub='h0')
    print('Offroad to hub retuned codes: {}'.format(res))


    speed = 0.1 # Speed for the robots to move at [0,1] where 1 is 100% speed
    cycle = 0 # Count the number of cycles the loop goes through
    while True:
        results = []
        sequences = []
        
        # For each project, use workstate 0 and a random hub, then call move to hub with that
        for project_name,info in project_info.items():
            workstate = info['workstates'][0]
            hubs = info['hubs']
            idx = random.randint(0,(len(hubs)-1))
            hub = hubs[idx]

            hub_res, hub_seq = cmdr.MoveToHub(workstate, hub, speed, project_name)
            sequences.append(hub_seq)
            results.append(hub_res)
        print("Move sequence numbers: {}".format(sequences))
        print("Move result codes: {}".format(results))

        # Wait for all moves to finish
        for move_idx in range(0,len(sequences)):
            cmdr.WaitForMove(sequences[move_idx])
                
        cycle += 1
        print("Completed %d cycles!"%(cycle))

        # Ensure that the socket pool remains at the set value (default is 25)
        print("Socket pool size: %d"%(len(cmdr._sockets)))

if __name__=="__main__":
    main()