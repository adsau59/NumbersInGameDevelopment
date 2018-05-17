"""
GDV_Python

Usage:
  GDV_Python.py [--svhost=<h>] [--svport=<p>]

Options:
  -h --help    Show this screen.
  --svhost=<h>     Specify IP of host (default 127.0.0.1)
  --svport=<p>     Specify port of host (default 12345)

"""

from docopt import docopt
import socket
import json
import matplotlib.pyplot as plt
import graphs
from copy import deepcopy
import time
from threading import Thread
import commands
import globals as g

arg = docopt(__doc__)
host = arg["--svhost"] if arg["--svhost"] is not None else "127.0.0.1"
port = int(arg["--svport"]) if arg["--svport"] is not None else 12345

connected = False


def communicate():
    global connected

    s = socket.socket()
    s.bind((host, port))

    print(host + ": waiting for connection")
    s.listen(1)
    while not g.exiting:
        connected = False
        c, addr = s.accept()
        connected = True
        message = (c.recv(4096)).decode('utf-8')
        c.close()
        if message == "quit":
            break
        startThreadAndAdd(Thread(target=handleJsonCall, args=(message,), name="Handle Json"))


def handleJsonCall(jsonStr):
    jsonDict = json.loads(jsonStr)

    if "datas" in jsonDict:

        if not g.configurationDone:
            g.dataList = []
            for i in jsonDict["datas"]:
                g.dataList.append([])
            g.dataListConfig = deepcopy(g.dataList)
            g.configurationDone = True

        if not g.graphOnline:
            return

        for i in range(0, len(jsonDict["datas"])):
            data = (float(jsonDict["datas"][i]))
            g.dataList[i].append(data)

    elif "graphType" in jsonDict:
        g.graphConfig = jsonDict


def console():
    time.sleep(1)

    while not g.exiting:
        cmd = input("?")
        for i in commands.commands:
            i.check(cmd)


def showGraph():
    while len(g.graphConfig) == 0 and not g.exiting:
        pass

    if g.exiting:
        return

    plt.ion()
    fig, g.ax = plt.subplots()
    graphs.config()

    g.graphOnline = True
    while g.graphOnline and not g.exiting:
        if g.clearGraph:
            g.ax.clear()
            graphs.config()

        # if len(g.dataList[0]) > 2:
        #     for i in range(0, len(g.dataList[0])):
        #         g.dataList[i] = g.dataList[i][-2:]

        graphs.plot()
        fig.canvas.flush_events()
        time.sleep(0.01)

    plt.close()


def startThreadAndAdd(thread):
    thread.start()
    g.threads.append(thread)


startThreadAndAdd(Thread(target=console, name="Console"))
startThreadAndAdd(Thread(target=communicate, name="Network"))
showGraph()

for x in g.threads:
    # print(x.getName() + " Exiting")
    if x.getName() == "Network" and not connected:
        # print("yo")
        s = socket.socket()
        s.connect((host, port))
        s.send("quit".encode('utf-8'))
        s.close()
    x.join()

print()
print("Thank you for using GDV_Unity,\nMade by Adam Saudagar")
