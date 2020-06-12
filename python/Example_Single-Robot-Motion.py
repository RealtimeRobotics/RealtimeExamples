#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
from lib.PythonCommanderHelper import PythonCommanderHelper
import lib.CommonOperations as cmn_ops 
import time
import random

def main():
    # Setup the PythonCommander which is responsible for sending commands to the 
    # RTR Controller that control the robot/s
    cmdr = PythonCommander("127.0.0.1", 9999)
    cmdr.Reconnect()

    # Setup the PytonCommanderHelper which is responsible for getting data from
    # the RTR Controller through its REST api
    helper = PythonCommanderHelper("127.0.0.1")
    group_info = helper.get_group_info()

    print("\nAvailable Groups:")
    for count,group_name in enumerate(group_info):
        print('\t' + str(count) + ' : ' + str(group_name))
    
    # Select a deconfliction group to control
    group_selected = False
    while not group_selected:
        selected_group = input('\nWhat group would you like to control: ')
        try:
            selected_projects_info = group_info[selected_group]
            group_selected = True
        except:
            print('Group not found in current groups loaded in Control Panel')

    # Load the selected group
    resp = helper.put_config_mode()
    resp = helper.put_load_group(selected_group)

    print('\nGroup loaded: %s'%(selected_group))
    print('Loaded projects: {}'.format(selected_projects_info['projects']))
    
    project_info = helper.get_project_info(group_info[selected_group])

    cmn_ops.startup_sequence(cmdr,project_info,selected_group)
    res = cmn_ops.put_on_roadmap(cmdr,project_info,selected_group)
    print(res)

    speed = 1.0
    cycle = 0
    while True:
        results = []
        sequences = []
        for project_name,info in project_info.items():
            workstate = info['workstates'][0]
            hubs = info['hubs']
            idx = random.randint(0,(len(hubs)-1))
            hub = hubs[idx]

            hub_res, hub_seq = cmdr.MoveToHub(workstate, hub, speed, project_name)
            sequences.append(hub_seq)
            results.append(hub_res)
            

        #TODO: launch these wait for moves in threads?
        print(sequences)
        print(results)
        cmdr.WaitForMove(sequences[0])
        cmdr.WaitForMove(sequences[1])
        cycle += 1
        print("Completed %d cycle!"%(cycle))
        print("Socket pool size: %d"%(len(cmdr._sockets)))

if __name__=="__main__":
    main()