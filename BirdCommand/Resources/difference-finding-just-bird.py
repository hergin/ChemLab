import sys
import networkx as nx
from networkx.algorithms import isomorphism as iso

graph_info = sys.argv[1]

pre_pattern = nx.DiGraph()
prePatternNodes = graph_info.split("|")[0]
if prePatternNodes.strip() != "":
    for node in prePatternNodes.split(","):
        id = node.split("-")[0]
        type = node.split("-")[1]
        pre_pattern.add_nodes_from([(id,{"type":type})])


prePatternEdges = graph_info.split("|")[1]
if prePatternEdges.strip() != "":
    for edge in prePatternEdges.split(","):
        id1 = edge.split("-")[0]
        id2 = edge.split("-")[1]
        type = edge.split("-")[2]
        pre_pattern.add_edges_from([(id1,id2,{"type":type})])


post_pattern = nx.DiGraph()
postPatternNodes = graph_info.split("|")[2]
if postPatternNodes.strip() != "":
    for node in postPatternNodes.split(","):
        id = node.split("-")[0]
        type = node.split("-")[1]
        post_pattern.add_nodes_from([(id,{"type":type})])

postPatternEdges = graph_info.split("|")[3]
if postPatternEdges.strip() != "":
    for edge in postPatternEdges.split(","):
        id1 = edge.split("-")[0]
        id2 = edge.split("-")[1]
        type = edge.split("-")[2]
        post_pattern.add_edges_from([(id1,id2,{"type":type})])


nodesToAdd = post_pattern.nodes()-pre_pattern.nodes()
for node in nodesToAdd:
    if node.startswith("Bird"):
        print(node)


nodesToDelete = pre_pattern.nodes() - post_pattern.nodes()
for node in nodesToDelete:
    if node.startswith("Bird"):
        print(node)