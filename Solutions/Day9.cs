using System.Collections;
using System.Text;

namespace Advent24.Solutions;

internal sealed class Day9 : Solution
{
    public Day9() : base(9)
    {
        
    }

    internal override string Part1()
    {
        int[] unpacked = [];
        for (var i = 0; i < Input.Length; i++)
        {
            var size = int.Parse(Input[i].ToString());
            if (i%2 == 0)
            {
                //file block
                var newFiles = Enumerable.Range(0, size).Select(c => i / 2);
                unpacked = [.. unpacked, .. newFiles];
                continue;
            }

            //empty block
            var newEmptyBlocks = Enumerable.Range(0, size).Select(c => -1);
            unpacked = [.. unpacked, .. newEmptyBlocks];
        }

        var defragmented = Defragment(unpacked);

        return CalculateChecksum(defragmented).ToString();
    }

    private int[] Defragment(int[] unpacked)
    {
        var list = unpacked.ToList();
        var firstEmpty = list.IndexOf(-1);
        var lastFile = list.FindLastIndex(c => c != -1);

        while (firstEmpty < lastFile)
        {
            //reorder
            var fileId = list[lastFile];
            list[firstEmpty] = fileId;
            list[lastFile] = -1;

            firstEmpty = list.IndexOf(-1);
            lastFile = list.FindLastIndex(c => c != -1);
        }

        return list.ToArray();
    }

    private long CalculateChecksum(int[] disk)
    {
        return disk.Select((f, i) => f == -1 ? 0 : (long)f * i).Sum();
    }

    internal override string Part2()
    {
        return "";
    }
}
