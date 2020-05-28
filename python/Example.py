#!/usr/bin/env python

from lib.PythonCommander import PythonCommander
import time


def main():
    print("Python Realtime Controller commander example.")
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
