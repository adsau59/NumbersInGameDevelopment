"""
Script for in program console commands
"""

import globals as g

class Command:
    def __init__(self, cmd, feedback, cmdArgsLength, func):
        self.cmd = cmd
        self.feedback = feedback
        self.cmdArgsLength = cmdArgsLength
        self.func = func

    def check(self, cmd):
        cmds = cmd.split(" ")
        if cmds[0] == self.cmd and (len(cmds[1:]) >= self.cmdArgsLength or self.cmdArgsLength == -1):
            self.run(cmds[1:])

    def run(self, cmdArgs):
        print(self.feedback)
        self.func(*cmdArgs)

def refresh():
    g.configurationDone = False
    g.clearGraph = True


def quit():
    g.exiting = True


def save(name):
    text = ""

    for i in range(0, len(g.dataList[0])):
        for col in g.dataList:
            text += str(col[i]) + ","
        text = text[:-1]
        text += "\n"

    fo = open(name + ".csv", "w")
    fo.write(text)
    fo.close()


commands = [
    Command("refresh", "refreshing...", 0, refresh),
    Command("save", "saving to file", 1, save),
    Command("quit", "exiting", 0, quit)
]
