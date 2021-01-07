using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20.Puzzle2
{
    static class ImageAssembler
    {
        public static Tile AssembleImage(Tile[] tiles) {
            var finalSize = (int) Math.Sqrt(tiles.Length); // Square image

            var assembled = new Tile[finalSize, finalSize];

            for (var row = 0; row < finalSize; row++) {
                for (var col = 0; col < finalSize; col++) {
                    switch (row) {
                        case 0 when col == 0:
                            assembled[0, 0] = SetCorner(tiles);
                            continue;
                        case 0:
                            assembled[0, col] = SetRightNeighbour(tiles, assembled[0, col - 1]);
                            continue;
                        default:
                            assembled[row, col] = SetBottomNeighbour(tiles, assembled[row - 1, col]);
                            break;
                    }
                }
            }

            for (var row = 0; row < finalSize; row++) {
                for (var col = 0; col < finalSize; col++) {
                    assembled[row, col] = assembled[row, col].RemoveEdges();
                }
            }

            return Combine(assembled);
        }

        private static Tile Combine(Tile[,] assembled) {
            var size = assembled[0, 0].Size;
            var rows = new List<string>();
            for (var row = 0; row < assembled.GetLength(0); row++) {
                var r2 = new List<string>();
                for (var i = 0; i < size; i++) {
                    var newRow = "";
                    for (var col = 0; col < assembled.GetLength(1); col++) {
                        newRow += assembled[row, col].Rows[i];
                    }

                    r2.Add(newRow);
                }

                rows.AddRange(r2);
            }

            return new Tile(0, rows.ToArray());
        }

        private static Tile SetCorner(Tile[] tiles) {
            var corner = tiles.First(t => t.GetNeighbours(tiles).Count == 2);
            while (corner.FindNeighbourWithEdge(corner.Right, tiles) == null || corner.FindNeighbourWithEdge(corner.Bottom, tiles) == null) {
                corner.Rotate();
            }

            return corner;
        }

        private static Tile SetRightNeighbour(Tile[] tiles, Tile tile) {
            var right = tile.FindNeighbourWithEdge(tile.Right, tiles);
            for (var i = 0; i < 4; i++) {
                if (IsMatch()) return right;
                // Check flipped
                right.FlipHorizontally();
                if (IsMatch()) return right;
                // Flip back
                right.FlipHorizontally();
                // ... and rotate
                right.Rotate();
            }

            throw new Exception("Invalid tile");

            bool IsMatch() {
                return tile.Right == right.Left;
            }
        }

        private static Tile SetBottomNeighbour(Tile[] tiles, Tile tile) {
            var bottom = tile.FindNeighbourWithEdge(tile.Bottom, tiles);
            for (var i = 0; i < 4; i++) {
                if (IsMatch()) return bottom;
                // Check flipped
                bottom.FlipVertically();
                if (IsMatch()) return bottom;
                // Flip back
                bottom.FlipVertically();
                // ... and rotate
                bottom.Rotate();
            }

            throw new Exception("Invalid tile");

            bool IsMatch() {
                return tile.Bottom == bottom.Top;
            }
        }
    }
}