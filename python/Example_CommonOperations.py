#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
from lib.PythonCommanderHelper import PythonCommanderHelper
import lib.CommonOperations as cmn_ops 
import time, random, sys, threading

from rtr_control_ros.srv import SetSimulatedError
from rtr_control_ros.srv import SetSimulatedErrorRequest

import rospy

def main():
    if len(sys.argv) != 1:
        ip_addr = str(sys.argv[1])
        print("Setting ip address of Realtime Controller to: %s"%(ip_addr))
    else:
        ip_addr = "127.0.0.1"

    # Setup the PythonCommander which is responsible for sending commands to the 
    # RTR Controller that control the robot/s or simulation
    cmdr = PythonCommander(ip_addr, 9999) #TODO: pass IP as argument.
    cmdr.Reconnect()

    # Commander helper that communicates with the Co
    helper = PythonCommanderHelper(ip_addr)

    # Enter Control Panel information here
    group = 'python_examples'
    project_info = {'python_library':{'workstates':['state_0'],'hubs':['home','forward']}}

    ################################################
    # Running cycle of common operations
    ################################################

    # Call startup sequence which calls InitGroup for each project and then BeginOperationMode
    print('Startup sequence...')
    time.sleep(0.75)

    cmn_ops.startup_sequence(cmdr,project_info,group)

    # Put each robot on the roadmap
    print('\nPutting robots on roadmap...')
    time.sleep(.75)

    cmn_ops.put_on_roadmap(cmdr,project_info,group,hub='home')

    # Simulate a fault using the ROS service
    print('\nSimulating a fault...')
    time.sleep(.75)

    helper.put_config_mode()
    helper.put_teleport_robot(list(project_info.keys())[0],'forward')

    srv_handle = rospy.ServiceProxy("/python_library/set_simulated_error", SetSimulatedError)
    request = SetSimulatedErrorRequest()
    request.simulated_error = True
    srv_handle(request)

    # Attempt to clear faults and put each robot back on the roadmap
    user_in = input('\nFault detected. Would you like to clear and home the robots? (y/n):')
    if (user_in == 'y') or (user_in == 'Y'):
        print('\nAttempting fault recovery...')
        time.sleep(0.75)
        cmn_ops.attempt_fault_recovery(cmdr,project_info,group,hub='home')
    else:
        print('Aborting. Leaving robot in fault state.')

if __name__=="__main__":
    main()