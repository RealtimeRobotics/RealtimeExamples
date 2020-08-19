# Python Examples
 ## Setup
 1. Install Python 3 and pip
 2. To use the REST api to get data from the controller install: 
```
pip3 install requests
```

 ## First Run
 1. In `Example.py` change the IP address to the IP address of the Realtime Controller you wish to command.
 1. `python Example.py`
 1. You should see the following output:
```Python Realtime Controller commander example.
    Sending: GetMode

    Response Code: 0, Data: CONFIG

    Sending: GetMode

    Response Code: 0, Data: CONFIG

    Sending: GetMode

    Response Code: 0, Data: CONFIG

    Sending: GetMode

    Response Code: 0, Data: CONFIG

    Sending: GetMode

    Response Code: 0, Data: CONFIG
```
## Common Operations
The following example is intended for release 1.2.0

1. Open the RTR Control panel and load the provided project `CommonOperationsExample.zip'
2. Create a Deconfliction Group called `CommonOperationsExample` and add the project to it
3. Load the group and verify the voxel region
4. Run `Example_CommonOperations.py` with no arguments since this example is intended for simulation
5. You should see the output
```
Sending: GetMode
GetMode,0,CONFIG

[INFO] Sent Get request to http://127.0.0.1/api/groups/
[INFO] Sent Get request to http://127.0.0.1/api/projects/CommonOperationsExample/

Startup sequence...
Sending: InitGroup,CommonOperationsExample,CommonOperationsExample,default_state
InitGroup,0
Sending: BeginOperationMode
BeginOperationMode,0,OPERATION

Putting robots on roadmap...
Sending: GetMode
GetMode,0,OPERATION
Sending: OffroadToHub,CommonOperationsExample,default_state,home,low,240.0,True
OffroadToHub,0,4
MoveResult,0,CommonOperationsExample,4

Would you like to test fault recovery? (y/n): y
Press and release the e-stop, then hit Enter...

Attempting fault recovery...
Sending: GetMode
GetMode,0,FAULT
Sending: ClearFaults
ClearFaults,0
Sending: InitGroup,CommonOperationsExample,CommonOperationsExample,default_state
InitGroup,0
Sending: BeginOperationMode
BeginOperationMode,0,OPERATION
Sending: GetMode
GetMode,0,OPERATION
Sending: OffroadToHub,CommonOperationsExample,default_state,home,low,240.0,True
OffroadToHub,0,5
MoveResult,0,CommonOperationsExample,5
```

## Next Steps
1. Use `Example.py` and `Example_CommonOperations.py` as a starting point to accomplish your needs.