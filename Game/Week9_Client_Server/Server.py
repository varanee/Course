import socket
import threading
import thread
import errno
import pickle
import time
import inspect

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
    lock = thread.allocate_lock()
    while 1:
        lock.acquire()

        data = client.recv(size)
        print "Send = " + data + " to "
        for c in list:
            c.send(data)
        lock.release()
        print "Release lock"

    s.close()
#threads = []


while 1:

    print "Waiting for some clients ..."
    client, address = s.accept()

    print "Client connected."
    print address

    list.append(client)

    print len(list)

    client.send("Hello!\n")

    t = threading.Thread(target=talk, args=(client,list, ))
   # threads.append(t)
    t.start()




