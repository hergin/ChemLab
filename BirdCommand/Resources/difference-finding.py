import networkx as nx
from networkx.algorithms import isomorphism as iso


pre_pattern = nx.Graph()
pre_pattern.add_nodes_from([
    ("0", {"type": "BD"}),
    ("1", {"type": "Em"}),
    ("2", {"type": "Em"}),
    ("3", {"type": "Em"})
])
pre_pattern.add_edges_from([("1", "2"),("1","0"),("2","3")])

post_pattern = nx.Graph()
post_pattern.add_nodes_from([
    ("0", {"type": "BD"}),
    ("1", {"type": "Em"}),
    ("2", {"type": "Em"})
])
post_pattern.add_edges_from([("1", "2"),("2","0")])


edgesToDelete = post_pattern.edges()-pre_pattern.edges()
print()
print("Edges To Delete")
print("--------")
for edge in edgesToDelete:
    print(edge)

edgesToAdd = pre_pattern.edges() - post_pattern.edges()
print()
print("Edges To Add")
print("--------")
for edge in edgesToAdd:
    print(edge)

nodesToDelete = post_pattern.nodes()-pre_pattern.nodes()
print()
print("Nodes To Delete")
print("--------")
for node in nodesToDelete:
    print(node)


nodesToAdd = pre_pattern.nodes() - post_pattern.nodes()
print()
print("Nodes To Add")
print("--------")
for node in nodesToAdd:
    print(node)
