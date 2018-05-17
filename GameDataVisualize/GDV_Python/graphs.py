"""
Script to draw graph
"""

import globals as g

def plot():

    if len(g.graphConfig["graphTypeString"]) == 0:
        return

    if g.graphConfig["graphType"] == "default":
        for i in range(0, int(len(g.dataList)/2)):
            try:
                graphTypeString = g.graphConfig["graphTypeString"][i]
            except IndexError:
                graphTypeString = "b-"
            g.ax.plot(g.dataList[i*2], g.dataList[i*2 + 1], graphTypeString)


def config():

    if g.graphConfig["graphType"] == "default":
        g.ax.set_xlabel(g.graphConfig["xLabel"])
        g.ax.set_ylabel(g.graphConfig["yLabel"])
        g.ax.set_title(g.graphConfig["title"])