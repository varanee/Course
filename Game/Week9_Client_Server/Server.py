import socket
import thread
import errno
import pickle

host = '' 
port = 16000
backlog = 5 
size = 1024
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.bind((host,port))
s.listen(backlog) 
list = []


def talk(client, list):
    while 1:
        data = client.recv(size)
        print "receive = " + data
        for c in list:
           client.sendto(data, (c[0],c[1]))

    s.close()

while 1:

    print "Waiting for some clients ..."
    client, address = s.accept()

    print "Client connected."
    print address

    list.append(address)

    print len(list)

    client.send("Hello!\n")

    try:

        thread.start_new_thread(talk,(client,list, ))

    except SocketError as e:
        if e.errno != errno.ECONNRESET:
            raise  # Not error we are looking for
        pass  # Handle error here.
