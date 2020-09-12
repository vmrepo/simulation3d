
#client sample for simulation3d

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
import struct

HOST = 'localhost'
PORT = 8888

class Context:
    pass

def log(s):

    s = datetime.strftime(datetime.now(), "%Y-%m-%d %H:%M:%S") + ' ' + s

    #with open(LOGFILE, 'a') as f:
    #    f.write(s + '\n')

    print(s)

def log_reduce(data):

    res = {}

    for k in data:

        if k in ['base64jpg', 'base64floats']:

            res[k] = (data[k][:6] + '.....' + data[k][-6:-1] + data[k][-1]) if len(data[k]) > 12 else data[k]

        else:
            res[k] = data[k]

    return res

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

    log('received ' + str(log_reduce(data)))

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

        send_packet(context, {'packet':'create', 'type':'camera', 'idname':'mycamera', 'x0':0, 'y0':0, 'z0':0, 'x1':0, 'y1':5, 'z1':5})
        data = receive_packet(context)

        send_packet(context, {'packet':'activecamera', 'idname':'mycamera'})
        data = receive_packet(context)

        send_packet(context, {'packet':'create', 'type':'manipulator1', 'idname':'manipulator1', 'x':-1, 'y':0, 'z':0})
        data = receive_packet(context)

        send_packet(context, {'packet':'create', 'type':'manipulator2', 'idname':'manipulator2', 'x':1, 'y':0, 'z':0, 'FingerDown':False})
        data = receive_packet(context)

        send_packet(context, {'packet':'setpos', 'idname':'manipulator1', 'a0':90, 'a1':45, 'a2':90})
        time.sleep(0.2)
        send_packet(context, {'packet':'setpos', 'idname':'manipulator2', 'a0':90, 'a1':45, 'a2':90, 'a3':90, 'a4':0, 'a5':45, 'a6':1})
        time.sleep(0.2)

        send_packet(context, {'packet':'gripped', 'idname':'manipulator2'})
        data = receive_packet(context)
        gripped = data['gripped']

        send_packet(context, {'packet':'create', 'type':'table', 'model':'conveyor1', 'idname':'conveyor', 'kinematic':False, 'x':0, 'y':1, 'z':1, 'ex':0, 'ey':0, 'ez':0, 'scale':1})
        data = receive_packet(context)

        send_packet(context, {'packet':'create', 'type':'thing', 'model':'juice1', 'idname':'juice', 'kinematic':True, 'x':0, 'y':2, 'z':0, 'ex':0, 'ey':0, 'ez':0, 'scale':1})
        data = receive_packet(context)
        send_packet(context, {'packet':'setpos', 'idname':'juice', 'a0':0, 'a1':2, 'a2':1, 'a3':0, 'a4':0, 'a5':0, 'a6':1, 'a7':0})
        time.sleep(2)
        send_packet(context, {'packet':'transform', 'idname':'conveyor'})
        data = receive_packet(context)
        send_packet(context, {'packet':'transform', 'idname':'juice'})
        data = receive_packet(context)

        send_packet(context, {'packet':'shoot'})
        data = receive_packet(context)
        if data['ok']:
            image = cv2.imdecode(np.fromstring(base64.b64decode(data['base64jpg']), dtype=np.uint8), -1)
            cv2.imwrite('shoot.jpg', image)

        send_packet(context, {'packet':'depth'})
        data = receive_packet(context)
        if data['ok']:
            data = base64.b64decode(data['base64floats'])
            w, h = 1280, 720
            image = np.zeros((h, w), np.float32)
            for y in range(h):
                for x in range(w): 
                    image[y, x] = struct.unpack('>f', data[(y * w + x) * 4 : (y * w + x) * 4  + 4])[0]
            image *= 10
            cv2.imwrite('depth.jpg', image)

        #send_packet(context, {'packet':'delete', 'idname':'manipulator1'})
        #data = receive_packet(context)

        #send_packet(context, {'packet':'clear'})
        #data = receive_packet(context)

        #send_packet(context, {'packet':'close'})

        send_packet(context, {'packet':'end'})

if __name__ == '__main__':
    main()
