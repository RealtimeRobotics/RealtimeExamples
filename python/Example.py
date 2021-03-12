#!/usr/bin/env python3

from lib.PythonCommander import PythonCommander
import time, sys


def main():
    print("Python Realtime Controller commander example.")

    ### If ip address is passed, use it
    if len(sys.argv) != 1:
        ip_addr = str(sys.argv[1])
    else: # Default IP address
        ip_addr = "127.0.0.1"
    print(f'Setting ip address of Realtime Controller to: {ip_addr}')

    commander = PythonCommander("127.0.0.1", 9999)
    if not commander.Reconnect():
        print("Error: unable to connect to RealtimeController")
        return

    for i in range(5):
        (code, data) = commander.GetMode()
        print('Response Code: {}, Data: {}'.format(code,data))
        time.sleep(1)


if __name__ == "__main__":
    main()
