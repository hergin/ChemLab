import sys
import networkx as nx
from networkx.algorithms import isomorphism as iso


model = nx.DiGraph()

model_node_string = sys.argv[1]
for node in model_node_string.split(","):
    id = node.split("-")[0]
    type = node.split("-")[1]
    model.add_nodes_from([(id,{"type":type})])

model_edge_string = sys.argv[2]
for edge in model_edge_string.split(","):
    id1 = edge.split("-")[0]
    id2 = edge.split("-")[1]
    type = edge.split("-")[2]
    model.add_edges_from([(id1,id2,{"type":type})])


pattern = nx.DiGraph()

pattern_node_string = sys.argv[3]
for node in pattern_node_string.split(","):
    id = node.split("-")[0]
    type = node.split("-")[1]
    pattern.add_nodes_from([(id,{"type":type})])

if len(sys.argv)>4:
    pattern_edge_String = sys.argv[4]
    if pattern_edge_String != "":
        for edge in pattern_edge_String.split(","):
            id1 = edge.split("-")[0]
            id2 = edge.split("-")[1]
            type = edge.split("-")[2]
            pattern.add_edges_from([(id1,id2,{"type":type})])

def node_match_bird(first, second):
    return first["type"] == second["type"]

def edge_match_bird(first, second):
    return first["type"] == second["type"]

matcher = iso.DiGraphMatcher(model, pattern, node_match=node_match_bird, edge_match=edge_match_bird)
print(str(matcher.subgraph_is_isomorphic()))

if matcher.subgraph_is_isomorphic():
    for key, value in list(matcher.subgraph_monomorphisms_iter())[0].items():
        print(key+"-"+value)