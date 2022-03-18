import sys
import networkx as nx
from networkx.algorithms import isomorphism as iso

model_node_string = sys.argv[1]
model_edge_string = sys.argv[2]
pattern_node_string = sys.argv[3]
pattern_edge_String = sys.argv[4]

model = nx.DiGraph()
for node in model_node_string.split(","):
    id = node.split("-")[0]
    type = node.split("-")[1]
    model.add_nodes_from([(id,{"type":type})])
for edge in model_edge_string.split(","):
    id1 = edge.split("-")[0]
    id2 = edge.split("-")[1]
    type = edge.split("-")[2]
    model.add_edges_from([(id1,id2,{"type":type})])


pattern = nx.DiGraph()
for node in pattern_node_string.split(","):
    id = node.split("-")[0]
    type = node.split("-")[1]
    pattern.add_nodes_from([(id,{"type":type})])
for edge in pattern_edge_String.split(","):
    id1 = edge.split("-")[0]
    id2 = edge.split("-")[1]
    type = edge.split("-")[2]
    pattern.add_edges_from([(id1,id2,{"type":type})])

def node_match_bird(first, second):
    return first["type"] == second["type"]

def edge_match_bird(first, second):
    return first["type"] == second["type"]

matcher = iso.GraphMatcher(model, pattern, node_match=node_match_bird, edge_match=edge_match_bird)
print("Pattern matches?: "+str(matcher.subgraph_is_isomorphic()))