// Suppose for our challange we have a 5x5 matrix, and the value of each cell is the cost of movement to that cell.
// The goal is to find the minimum cost path from the top-left corner to the bottom-right corner of the matrix.
// We can only move in a CROSS pattern: up, down, left, or right (no diagonal moves allowed).
/*
    { (1), 3, 5, 7, 2 },
    { 2, 4, 6, 8, 1 },
    { 5, 2, 1, 9, 4 },
    { 3, 7, 2, 5, 8 },
    { 4, 1, 6, 3, (9) }
*/

int[,] matrix1 = new int[,]
{
    { 1, 3, 5, 7, 2 },
    { 2, 4, 6, 8, 1 },
    { 5, 2, 1, 9, 4 },
    { 3, 7, 2, 5, 8 },
    { 4, 1, 6, 3, 9 }
}; 
int[,] matrix2 = new int[,]
{
    { 1, 3, 5, 7, 2 },
    { 2, 4, 6, 8, 1 },
    { 5, 2, 1, 1, 4 },
    { 3, 7, 2, 1, 8 },
    { 4, 1, 6, 3, 9 }
}; 
int[,] matrix3 = new int[,]
{
    { 1, 3, 5, 7, 2 },
    { 2, 4, 6, 8, 1 },
    { 5, 9, 9, 9, 4 },
    { 3, 7, 9, 5, 8 },
    { 4, 1, 6, 3, 9 }
};

int[,] matrixOfChoice = matrix3;

// Using Greedy algorithm to find the minimum cost path with suboptimal choices

int MinCostPath(int[,] grid)
{
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);
    int totalCost = grid[0, 0];
    int x = 0, y = 0;
    while (x < rows - 1 || y < cols - 1)
    {
        int downCost = (x + 1 < rows) ? grid[x + 1, y] : int.MaxValue;
        int rightCost = (y + 1 < cols) ? grid[x, y + 1] : int.MaxValue;
        if (downCost < rightCost)
        {
            totalCost += downCost;
            x++;
        }
        else
        {
            totalCost += rightCost;
            y++;
        }
    }
    return totalCost;
}

int result = MinCostPath(matrixOfChoice);
Console.WriteLine($"Minimum cost path (Greedy): {result}");
// Note: This greedy approach does not guarantee the optimal solution for all matrices.
// For an optimal solution, a dynamic programming approach would be more appropriate.


// Using Dynamic Programming to find the optimal minimum cost path with dijkstra's algorithm

int MinCostPathDP(int[,] grid)
{
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);
    int[,] cost = new int[rows, cols];
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            cost[i, j] = int.MaxValue;
        }
    }
    cost[0, 0] = grid[0, 0];
    var directions = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };
    var pq = new PriorityQueue<(int x, int y), int>();
    pq.Enqueue((0, 0), grid[0, 0]);
    while (pq.Count > 0)
    {
        var current = pq.Dequeue();
        int currentCost = cost[current.x, current.y];
        int x = current.x;
        int y = current.y;
        foreach (var dir in directions)
        {
            int newX = x + dir.Item1;
            int newY = y + dir.Item2;
            if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
            {
                int newCost = currentCost + grid[newX, newY];
                if (newCost < cost[newX, newY])
                {
                    cost[newX, newY] = newCost;
                    pq.Enqueue((newX, newY), newCost);
                }
            }
        }
    }
    return cost[rows - 1, cols - 1];
}

int optimalResult = MinCostPathDP(matrixOfChoice);
Console.WriteLine($"Minimum cost path (Dynamic Programming): {optimalResult}");