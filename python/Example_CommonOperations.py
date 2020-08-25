#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
from lib.PythonCommanderHelper import PythonCommanderHelper
import lib.CommonOperations as cmn_ops 
import time, random, sys

def main():
    ### If ip address is passed, use it
    if len(sys.argv) != 1:
        ip_addr = str(sys.argv[1])
        print(f'Setting ip address of Realtime Controller to: {ip_addr}')
    else: # Default IP address
        ip_addr = "127.0.0.1"

    # Setup the PythonCommander which is responsible for sending commands to the 
    # RTR Controller that control the robot/s or simulation
    cmdr = PythonCommander(ip_addr, 9999) #TODO: pass IP as argument.
    cmdr.Reconnect()

    code, data = cmdr.GetMode()
    if data == 'FAULT':
        print('Clear faults first!')
        return
    
    # Commander helper that communicates with the controller using a REST api
    helper = PythonCommanderHelper(ip_addr)

    ### Request the group info from the control panel and then determine which group is loaded
    group_info = helper.get_group_info()
    group = None
    for group_name,info in group_info.items():
        if info['loaded'] == True:
            group = group_name
            break
    if group:
        print(f'\n{group} is loaded.')
    else:
        print(f'\nNo group is loaded!')
        return

    # Nested dictionary that contains all projects information of the form:
    # {project name: {workstates: [str], hubs: [str]}
    project_info = helper.get_project_info(group_info[group]['projects'])
    project_names = group_info[group]['projects']

    ################################################
    # Running cycle of common operations
    ################################################

    # Call startup sequence which calls InitGroup for each project and then BeginOperationMode
    print('Startup sequence...')
    time.sleep(1.0)

    cmn_ops.startup_sequence(cmdr,project_info,group)

    # Put each robot on the roadmap
    print('\nPutting robots on roadmap...')
    time.sleep(1.0)

    cmn_ops.put_on_roadmap(cmdr,project_info,group,hub='home')

    # Clear robot faults programatically, and then put each robot back on the roadmap
    user_in = input('\nWould you like to test fault recovery? (y/n): ')
    if (user_in == 'y') or (user_in == 'Y'):
        user_in = input('\nPress and release the e-stop, then hit Enter...')
        print('\nAttempting fault recovery...')
        cmn_ops.attempt_fault_recovery(cmdr,project_info,group,hub='home')
    else:
        print('Skipping fault recovery test.')

if __name__=="__main__":
    main()