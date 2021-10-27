using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGraph : Graph<TilemapNode> {
    private Tilemap tilemap;

    void Start() {
        tilemap = this.GetComponent<Tilemap>();

        MakeNodes();
    }

    private void MakeNodes() {
        int xSize = tilemap.cellBounds.xMax - tilemap.cellBounds.xMin;
        int ySize = tilemap.cellBounds.yMax - tilemap.cellBounds.yMin;
        int xOffset = tilemap.cellBounds.xMin;
        int yOffset = tilemap.cellBounds.yMin;

        TilemapNode[,] nodeMatrix = new TilemapNode[xSize, ySize];

        for(int y = yOffset; y < yOffset + ySize; y++) {
            for(int x = xOffset; x < xOffset + xSize; x++) {
                TilemapNode node;
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if(tilemap.HasTile(tilePosition)) {
                    Vector3 position = tilemap.CellToWorld(tilePosition) + tilemap.cellSize / 2.0f;
                    position.z = 0;

                    node = new TilemapNode(position);
                    nodes.Add(node);
                } else {
                    node = null;
                }

                nodeMatrix[x-xOffset, y-yOffset] = node;
            }
        }

        for(int y = 0; y < nodeMatrix.GetLength(1); y++) {
            for(int x = 0; x < nodeMatrix.GetLength(0); x++) {
                TilemapNode node = nodeMatrix[x, y]; 

                if(node == null) continue;

                for(int i = 0; i < 4; i++) {
                    int ox, oy;
                    if(i < 2) {
                        ox = 0;
                        oy = i * 2 - 1;
                    } else if(i < 4) {
                        ox = (i - 2) * 2 - 1;
                        oy = 0;
                    } else {
                        ox = oy = 0;
                    }
                    
                    int dx = x + ox;
                    int dy = y + oy;

                    if(dx > 0 && dy > 0 && dx < nodeMatrix.GetLength(0) && dy < nodeMatrix.GetLength(1)) {
                        TilemapNode otherNode = nodeMatrix[dx, dy];

                        if(otherNode != null) {
                            node.AddNeighbor(otherNode);
                        }
                    }
                }
            }
        }
    }
}
