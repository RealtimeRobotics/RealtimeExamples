# C# Examples
## Setup
1. Install .NET Core:
    * https://dotnet.microsoft.com/download


## First Run
1. In `Program.cs` change the IP Address to the IP address of the Realtime Controller you wish to command.
1. In a terminal window, navigate to the `csharp_commander` folder.
1. `dotnet run`
1. You should see the following output:
    ```Starting TCP Server.
    TCP client connected.
    Listening...
    Sending: GetMode
    Response: GetMode,SUCCESS,CONFIG
    Sending: GetMode
    Response: GetMode,SUCCESS,CONFIG
    Sending: GetMode
    Response: GetMode,SUCCESS,CONFIG
    Sending: GetMode
    Response: GetMode,SUCCESS,CONFIG
    Sending: GetMode
    Response: GetMode,SUCCESS,CONFIG

## Next Steps
1. Use `Program.cs` as an example to accomplish your needs.
1. Additional Command classes can be found in `csharp_commander/lib/Commands`.
