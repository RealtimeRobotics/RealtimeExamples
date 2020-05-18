#!/usr/bin/env python

from lib.PythonCommander import PythonCommander
import time

group_name = "test"
project_name = "Test_0"
workstate = "default_state"

def main():
    cmdr = PythonCommander("127.0.0.1", 9999)
    cmdr.Reconnect()
    cmdr.Setup(group_name, project_name)
    cmdr.InitGroup(workstate)
    cmdr.BeginOperation()
    
    hub_res, hub_seq = cmdr.OffroadToHub(workstate, "home", "low", 30.0, True, project_name)
    print("hub_res {}, hub_seq {}".format(hub_res, hub_seq))

    resp = cmdr.WaitForMove(hub_seq)

    hubs = ['forward_1','forward_2']
    for hub in hubs:
        hub_res, hub_seq = cmdr.MoveToHub(workstate, hub, 0.1, project_name)
        print("hub_res {}, hub_seq {}".format(hub_res, hub_seq))
        resp = cmdr.WaitForMove(hub_seq)
    
    hub_res, hub_seq = cmdr.MoveToHub(workstate, "home", 0.1, project_name)
    print("hub_res {}, hub_seq {}".format(hub_res, hub_seq))
    resp = cmdr.WaitForMove(hub_seq)


if __name__=="__main__":
    main()