# Chat app in dotnetcore

This single file chat app was built to acquaint a first year computer science student with basics of socket programming. It has been written with dotnetcore SDK and can be run on Linux, Windows and Mac without any changes.
The project can be run using the command `dotnet build && dotnet run`. You must have dotnetcore sdk installed in your
computer for this to work.

How to run?
------------

This program is supposed to be run on two connected computers. Computers connected on same WiFi network are recommended. You can also connect two laptops through a LAN cable and run this after setting up networking between them. When you run, you need to choose the first option on one computer, i.e. listen for connections. Second option should be chosen
on second computer i.e. connect to an IP address, and the ip address of first computer must be provided for a connection to be established.

Things to do
-----------------

Following tasks need to be done and is left as an exercise for the adapter.

1. Separate the received messages pane with input pane.
2. Fix messup of console colors. 
3. False input hardening.
4. Port this to various programming languages. Start with Java, followed by Ruby and Python. Then take more challenge and do it in C++ and C.