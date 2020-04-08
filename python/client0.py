
import os
import sys
import json
import socket
import select
import time
from datetime import datetime
from collections import deque
import numpy as np
import random
import base64
import cv2

HOST = 'localhost'
PORT = 8887

class Context:
    pass

def log(s):

    s = datetime.strftime(datetime.now(), "%Y-%m-%d %H:%M:%S") + ' ' + s

    #with open(LOGFILE, 'a') as f:
    #    f.write(s + '\n')

    print(s)

def receive_packet(context, blocking = True):

    buf = context.conn.recv(1024).decode() if blocking else ''

    while True:
        ready = select.select([context.conn], [], [], 0)
        if not ready[0]:
            break
        r = context.conn.recv(1024).decode()
        if r == '':
            break
        buf += r

    pos = 0
    while True:
        packet = None
        if buf[pos:4] == 'json':
            pos1 = buf.find(':', pos)
            if pos1 != -1:
                size = int(buf[pos + 4:pos1])
                pos1 += 1
                pos = pos1 + size
                while pos > len(buf):
                    r = context.conn.recv(1024).decode()
                    buf += r
                packet = buf[pos1:pos]
        if packet == None:
            break
        context.packetdeque.append(packet)

    json_data = context.packetdeque.popleft() if len(context.packetdeque) != 0 else None

    if not json_data:
        return None

    data = json.loads(json_data)

    log('received ' + str(data))

    return data

def send_packet(context, data):
    json_data = json.dumps(data)
    json_data = 'json' + str(len(json_data)) + ':' + json_data
    context.conn.send(json_data.encode())
    log('sent ' + str(data))

def main():

    context = Context()
    context.conn = socket.socket()
    context.conn.connect((HOST, PORT))
    context.packetdeque = deque()

    data = receive_packet(context)

    if data['packet'] == 'ready':

        send_packet(context, {'packet':'setcamera', 'x0':0, 'y0':0, 'z0':0, 'x1':0, 'y1':5, 'z1':5})
        data = receive_packet(context)

        send_packet(context, {'packet':'create', 'type':'manipulator1', 'x':-1})
        data = receive_packet(context)

        send_packet(context, {'packet':'create', 'type':'manipulator2', 'x':1})
        data = receive_packet(context)

        send_packet(context, {'packet':'setpos', 'id':1, 'a0':90, 'a1':45, 'a2':90})
        data = receive_packet(context)

        send_packet(context, {'packet':'setpos', 'id':2, 'a0':90, 'a1':45, 'a2':90})
        data = receive_packet(context)

        #send_packet(context, {'packet':'delete', 'id':1})
        #data = receive_packet(context)

        send_packet(context, {'packet':'end'})

if __name__ == '__main__':
    main()