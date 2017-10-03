""" 
A simple echo server,
to test tcp networking code
""" 

import socket 

host = '' 
port = 16000
backlog = 5 
size = 1024 
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.bind((host,port)) 
s.listen(backlog) 

while 1:
    client, address = s.accept() 
    print "Client connected."
    client.send("Hello!\n") 

    while 1: 
        data = client.recv(size)
        print "receive = "+data
