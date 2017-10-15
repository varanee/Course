#https://stackoverflow.com/questions/3757289/tcp-option-so-linger-zero-when-its-required
import socket
import threading
import thread
import errno
import pickle
import time
import inspect
import struct

host = ''
port = 16000
backlog = 5
size = 1024
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.bind((host, port))
s.listen(backlog)
list = []


def talk(client, list):
    lock = thread.allocate_lock()
    try:
        while 1:
            lock.acquire()
            data = client.recv(size)
            print "Send = " + data + " to "
            for c in list:
                c.send(data)
            lock.release()
            print "Release lock"
    except:
        list.remove(client)
        handler(client, address)
    #finally:
    #    s.close()
    #    lock.acquire()
    #    list.remove(client)
    #    lock.release()
    #    print "sock close "+str(len(list))

#threads = []
def handler(client_sock, addr):
    try:
        print('disconnected client from %s:%s' % addr)
        time.sleep(1)
    finally:
        client_sock.setsockopt(socket.SOL_SOCKET, socket.SO_LINGER,
            struct.pack('ii', 1, 0))
        client_sock.close()   # close current connection directly


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
