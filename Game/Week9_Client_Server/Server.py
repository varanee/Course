import socket
import threading
import json
import time
import struct
from Queue import Queue

host = ''
port = 16000
backlog = 5
size = 1024
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.bind((host, port))
s.listen(backlog)
list = []
dict = {}
myQueue = Queue()
data = ''

def talk( client, list, dict, address):
    try:
        data = ''
        while 1:
            data = client.recv(size)
            if not data: 
                break
            for c in list:
                if c != client:
                    print data
                    c.send(data)
        raise Exception('client disconnect')
    except Exception as e:
        json_string = '{"x":-5.0,"y":3.0,"n":"'+dict[address]+'","a":0}'
        parsed_json = json.dumps(json_string) #This both line remove \ from json
        return_json = json.loads(parsed_json)
        for c in list:
            if c != client:
                c.send(return_json+"\n")
        time.sleep(1)
        list.remove(client)
        dict.pop(address, None)
        print "list"+str(list)
        print "Get queue"+str(myQueue.get())
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
    print "No. of clients = "+str(len(list))

    client.send("Hello!\n")
    data = client.recv(size)
    dict[address] = data
    print "User = "+dict[address] + " is connected."

    t = threading.Thread(target=talk, args=(client,list,dict,address ))
    t.daemon = True
    t.start()
    myQueue.put(t)
    