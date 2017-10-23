#https://stackoverflow.com/questions/3757289/tcp-option-so-linger-zero-when-its-required
import socket
import threading
import thread
import errno
import json
import time
import inspect
import struct
from Queue import Queue

#from signal import signal, SIGPIPE, SIG_DFL
#signal(SIGPIPE, SIG_DFL)

host = ''
port = 16000
backlog = 5
size = 1024
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.bind((host, port))
s.listen(backlog)
list = []
myQueue = Queue()

def talk( client, list):
    #lock = thread.allocate_lock()
    data = ''
    try:
        while 1:
            #lock.acquire()
            data = client.recv(size)
            if not data: 
                break
           # print client
            for c in list:
           #     print data
                if c != client:
                    c.send(data)
            #lock.release()
            #time.sleep(0.01)
        raise Exception('client disconnect')
    except Exception as e:
        print "".format(e)
        if data:
            jsonData = json.loads(data)
            jsonData['a'] = 0
            json_return = json.dumps(jsonData)
            print "json_return "+json_return
        list.remove(client)
        print "list"+str(list)
        print "Get queue"+str(myQueue.get())
                    
        #lock.acquire()
        if len(list) != 0:
            for c in list:
                c.send(json_return)
        #lock.release()
        handler(client, address)

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
    t.daemon = True
    t.start()
    myQueue.put(t)
    