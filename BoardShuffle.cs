using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15puzzle
{
    public class BoardShuffle
    {
        // Fisher-Yates algorithm
        // @see https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        public int[] Shuffle()
        {
            int[] numberArray = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
            Random rnd = new Random();

            int n = numberArray.Length;

            while (n > 1)       // finishes when it reaches the "first"[0] element of the array
            {
                int k = rnd.Next(n--);      // 15, 14, 13, 12...
                int temp = numberArray[n];  // temporary holds the value at the index n
                numberArray[n] = numberArray[k];    // the index n has the random value of k (within the range) [the original is held in temp]
                numberArray[k] = temp;      // and now the random index has the value of the temporary [it switches places]
            }

            return numberArray;
        }

        // testing purposes
        public void DisplayShuffledNumbers(int[] numbers)
        {
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
