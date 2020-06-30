#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
from lib.PythonCommanderHelper import PythonCommanderHelper
import lib.CommonOperations as cmn_ops 
import time, random, sys, threading

from rtr_control_ros.srv import SetSimulatedError
from rtr_control_ros.srv import SetSimulatedErrorRequest

import rospy

def main():
    ### If ip address is passed, use it
    if len(sys.argv) != 1:
        ip_addr = str(sys.argv[1])
        print("Setting ip address of Realtime Controller to: %s"%(ip_addr))
    else: # Default IP address
        ip_addr = "127.0.0.1"

    # Setup the PythonCommander which is responsible for sending commands to the 
    # RTR Controller that control the robot/s or simulation
    cmdr = PythonCommander(ip_addr, 9999) #TODO: pass IP as argument.
    cmdr.Reconnect()

    # Commander helper that communicates with the Co
    helper = PythonCommanderHelper(ip_addr)

    ### Enter Control Panel information here
    # group name
    group = 'CommonOperationsExample'

    # Nested dictionary that contains all projects information of the form:
    #   project name: str
    #       workstates: [str]
    #       hubs: [str]
    group_info = helper.get_group_info()

    project_info = helper.get_project_info(group_info[group]['projects'])#['CommonOperationsExample'])
    project_names = group_info[group]['projects'] #list(project_info.keys())

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

    # Teleport the robot to a random hub and then simulate a fault using the ROS service
    print('\nSimulating a fault...')
    time.sleep(1.0)

    helper.put_config_mode()
    hubs = project_info[project_names[0]]['hubs']
    helper.put_teleport_robot(project_names[0],hubs[random.randint(0,len(hubs)-1)])

    srv_handle = rospy.ServiceProxy("/%s/set_simulated_error"%(project_names[0]), SetSimulatedError)
    request = SetSimulatedErrorRequest()
    request.simulated_error = True
    srv_handle(request)

    # Attempt to clear faults and put each robot back on the roadmap
    user_in = input('\nFault detected. Would you like to clear and home the robots? (y/n): ')
    if (user_in == 'y') or (user_in == 'Y'):
        print('\nAttempting fault recovery...')
        time.sleep(0.75)
        cmn_ops.attempt_fault_recovery(cmdr,project_info,group,hub='home')
    else:
        print('Aborting. Leaving robot in fault state.')

if __name__=="__main__":
    main()