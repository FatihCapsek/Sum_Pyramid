using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

namespace Sum_Pyramid
{
    public static class Program
    {
        private const string input_value = @" 215
              193 124
              117 237 442
              218 935 347 235
              320 804 522 417 345
              229 601 723 835 133 124
              248 202 277 433 207 263 257
              359 464 504 528 516 716 871 182
              461 441 426 656 863 560 380 171 923
              381 348 573 533 447 632 387 176 975 449
              223 711 445 645 245 543 931 532 937 541 444
              330 131 333 928 377 733 017 778 839 168 197 197
              131 171 522 137 217 224 291 413 528 520 227 229 928
              223 626 034 683 839 053 627 310 713 999 629 817 410 121
              924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

        public static void Main(string[] args)
        {
            int[,] convertedTriangle2 = input_value.ConvertTo2DArray();
            int maxSum2 = convertedTriangle2.MoveDownwards();
            Console.WriteLine("Input value: "+input_value);
            Console.WriteLine("The result for input value : " + maxSum2);

            Console.WriteLine("Enter the file path: (try " + @"pyramid.txt" + ")");
            string cs = Console.ReadLine();
            string contents = File.ReadAllText(cs);             

            int[,] convertedTriangleFile = contents.ConvertTo2DArray();
            int maxSumX = convertedTriangleFile.MoveDownwards();
            Console.WriteLine("The result from File " + maxSumX);
            Console.ReadLine();
        }

        private static int MoveDownwards(this int[,] matrixOfTriangle)
        {
            int length = matrixOfTriangle.GetLength(0);

            int res = -1;
            for (int i = 0; i < length - 2; i++)
                res = Math.Max(res, matrixOfTriangle[0, i]);

            for (int i = 1; i < length; i++)
            {
                res = -1;
                for (int j = 0; j < length; j++)
                {
                    if (j == 0 && matrixOfTriangle[i, j] != -1)
                    {
                        if (matrixOfTriangle[i - 1, j] != -1)
                            matrixOfTriangle[i, j] += matrixOfTriangle[i - 1, j];
                        else
                            matrixOfTriangle[i, j] = -1;
                    }
                    else if (j > 0 && j < length - 1 && matrixOfTriangle[i, j] != -1)
                    {
                        int tmp = CalculateNode(matrixOfTriangle[i - 1, j],
                                   matrixOfTriangle[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrixOfTriangle[i, j] = -1;
                        }
                        else
                            matrixOfTriangle[i, j] += tmp;
                    }

                    else if (j > 0 && matrixOfTriangle[i, j] != -1)
                    {
                        int tmp = CalculateNode(matrixOfTriangle[i - 1, j],
                                         matrixOfTriangle[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrixOfTriangle[i, j] = -1;
                        }
                        else
                            matrixOfTriangle[i, j] += tmp;
                    }
                    else if (j != 0 && j < length - 1 && matrixOfTriangle[i, j] != -1)
                    {
                        int tmp = CalculateNode(matrixOfTriangle[i - 1, j],
                                     matrixOfTriangle[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrixOfTriangle[i, j] = -1;
                        }
                        else
                            matrixOfTriangle[i, j] += tmp;
                    }
                    res = Math.Max(matrixOfTriangle[i, j], res);
                }
            }
            return res;
        }

        private static int[,] ConvertTo2DArray(this string input)
        {
            string[] array = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); 

            int[,] matrixOfTriangle = new int[array.Length, array.Length + 1];      

            for (var row = 0; row < array.Length; row++)
            {
                int[] digitsInRow = Regex.Matches(array[row], "[0-9]+")
                    .Cast<Match>()
                    .Select(m => int.Parse(m.Value)).ToArray();

                for (var column = 0; column < digitsInRow.Length; column++)
                {
                    matrixOfTriangle[row, column] = digitsInRow[column];
                }
            }
            return matrixOfTriangle.RemovePrimeNumbers();  
        }

        private static int CalculateNode(int input1, int input2)  
        {
            if (input1 == -1 && input2 == -1 || input1 == 0 && input2 == 0)
                return -1;
            else
                return Math.Max(input1, input2);
        }

        private static bool Is_Prime(this int number)
        {
            if (number == 1 | number == 0)
                return false;

            for(int i =2;i<number/2;i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private static int[,] RemovePrimeNumbers(this int[,] matrixOfTriangle)
        {
            int length = matrixOfTriangle.GetLength(0);
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    if (matrixOfTriangle[i, j] == 0)
                    {
                        continue;
                    }
                    else if (Is_Prime(matrixOfTriangle[i, j]))      
                    {
                        matrixOfTriangle[i, j] = -1;
                    }
                }
            }
            return matrixOfTriangle;
        }
    }
}
