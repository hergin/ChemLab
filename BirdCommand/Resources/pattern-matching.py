import networkx as nx
from networkx.algorithms import isomorphism as iso

def read_and_produce_model(fileName):

    model = nx.Graph()
    f = open(fileName)
    
    # add nodes
    rowIndex = 0
    for line in f.readlines():
        columnIndex = 0
        for cell in line.split("-"):
            if cell.startswith("B"):
                model.add_nodes_from([(str(columnIndex)+"_"+str(rowIndex),{"type":"Em"})])
                model.add_nodes_from([("BirdNode",{"type":cell.strip()})])
                model.add_edge("BirdNode",str(columnIndex)+"_"+str(rowIndex))
            elif cell == "Pi":
                model.add_nodes_from([(str(columnIndex)+"_"+str(rowIndex),{"type":"Em"})])
                model.add_nodes_from([("PigNode",{"type":cell.strip()})])
                model.add_edge("PigNode",str(columnIndex)+"_"+str(rowIndex))
            else:
                model.add_nodes_from([(str(columnIndex)+"_"+str(rowIndex),{"type":cell.strip()})])
            columnIndex = columnIndex + 1
        rowIndex = rowIndex + 1
    
    # add edges in each row
    for k in range(0,8): # go for each row
        for i in range(0,7): # add edges for the kth row (one less edge than the node number)
            model.add_edge(str(i)+"_"+str(k),str(i+1)+"_"+str(k))
    
    # add edges in each column
    for k in range(0,8): # go for each column
        for i in range(0,7): # add edges for the kth column (one less edge than the node number)
            model.add_edge(str(k)+"_"+str(i),str(k)+"_"+str(i+1))

    return model



maze1 = read_and_produce_model("hoc1.txt")

pre_pattern = nx.Graph()
pre_pattern.add_nodes_from([
    ("0", {"type": "BD"}),
    ("1", {"type": "Em"}),
    ("2", {"type": "Em"}),
    ("3", {"type": "Em"})
])
pre_pattern.add_edges_from([("1", "2"),("1","0"),("2","3")])

def node_match_bird(first, second):
    return first["type"] == second["type"]

def edge_match_bird(first, second):
    return True

matcher = iso.GraphMatcher(maze1, pre_pattern, node_match=node_match_bird, edge_match=edge_match_bird)
print("Pattern matches?: "+str(matcher.subgraph_is_isomorphic()))

print()
print("Mappings")
print("--------")
for key in matcher.mapping:
    print(key+": "+matcher.mapping[key])
