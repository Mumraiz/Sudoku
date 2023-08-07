class Program
{
    static void Main()
    {
        int[][] goodSudoku1 = {
            new int[] {7,8,4,  1,5,9,  3,2,6},
            new int[] {5,3,9,  6,7,2,  8,4,1},
            new int[] {6,1,2,  4,3,8,  7,5,9},
            new int[] {9,2,8,  7,1,5,  4,6,3},
            new int[] {3,5,7,  8,4,6,  1,9,2},
            new int[] {4,6,1,  9,2,3,  5,8,7},
            new int[] {8,7,6,  3,9,4,  2,1,5},
            new int[] {2,4,3,  5,6,1,  9,7,8},
            new int[] {1,9,5,  2,8,7,  6,3,4}
        };
        int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

        int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

        int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

        bool isValid = ValidateSudoku(goodSudoku1);
        Console.WriteLine(isValid);
        bool isValid1 = ValidateSudoku(goodSudoku2);
        Console.WriteLine(isValid1);
        bool isValid2 = ValidateSudoku(badSudoku1);
        Console.WriteLine(isValid2);
        bool isValid3 = ValidateSudoku(badSudoku2);
        Console.WriteLine(isValid3);
    }

    public static bool ValidateSudoku(int[][] sudoku)
    {
        int n = sudoku.Length;
        int sqrtN = (int)Math.Sqrt(n);

        if (!IsValidDimension(n, sqrtN))
            return false;

        return ValidateRows(sudoku, n) && ValidateColumns(sudoku, n) && ValidateSquares(sudoku, n, sqrtN);
    }

    private static bool IsValidDimension(int n, int sqrtN)
    {
        return n > 0 && sqrtN * sqrtN == n;
    }

    private static bool ValidateRows(int[][] sudoku, int n)
    {
        return sudoku.All(row => row.Length == n && IsDistinct(row) && IsWithinRange(row, n));
    }

    private static bool ValidateColumns(int[][] sudoku, int n)
    {
        for (int col = 0; col < n; col++)
        {
            int[] column = new int[n];
            for (int row = 0; row < n; row++)
                column[row] = sudoku[row][col];

            if (!IsDistinct(column) || !IsWithinRange(column, n))
                return false;
        }
        return true;
    }

    private static bool ValidateSquares(int[][] sudoku, int n, int sqrtN)
    {
        for (int row = 0; row < n; row += sqrtN)
        {
            for (int col = 0; col < n; col += sqrtN)
            {
                int[] subgrid = new int[n];
                int index = 0;
                for (int i = row; i < row + sqrtN; i++)
                {
                    for (int j = col; j < col + sqrtN; j++)
                    {
                        subgrid[index++] = sudoku[i][j];
                    }
                }

                if (!IsDistinct(subgrid) || !IsWithinRange(subgrid, n))
                    return false;
            }
        }
        return true;
    }

    private static bool IsDistinct(int[] values)
    {
        return values.Distinct().Count() == values.Length;
    }

    private static bool IsWithinRange(int[] values, int n)
    {
        return values.All(value => value >= 1 && value <= n);
    }
}
