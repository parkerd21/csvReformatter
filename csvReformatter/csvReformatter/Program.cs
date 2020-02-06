// Parker Downing
// Henry Schein Take Home Test
// csvConverter
using System;
using System.Collections.Generic;

namespace csvReformatter
{
    class Program
    {
        static void Main(string[] args)
        {
            string sampleInput = 
            @"""Patient Name"",""SSN"",""Age"",""Phone Number"",""Status""
""Prescott, Zeke"",""542-51-6641"",21,""801-555-2134"",""Opratory=2,PCP=1""
""Goldstein, Bucky"",""635-45-1254"",42,""435-555-1541"",""Opratory=1,PCP=1""
""Vox, Bono"",""414-45-1475"",51,""801-555-2100"",""Opratory=3,PCP=2""";
            
            Console.WriteLine(Reformatter(sampleInput));
        }

        static string Reformatter(string csvData)
        {
            bool leftBracketFlag = false;
            bool commaFlag = false;
            bool rightBracketFlag = false;
            List<Char> charData = new List<Char>(csvData);
            for (int index = 0; index < charData.Count; index++)
            {
                if (charData[index] == '"' && !leftBracketFlag && !rightBracketFlag) 
                {
                    charData[index] = '['; // change " to [
                    leftBracketFlag = true;
                }
                else if (charData[index] == '"' && leftBracketFlag)
                {
                    charData[index] = ']'; // change " to ]
                    rightBracketFlag = true;
                    leftBracketFlag = false;
                }
                else if (charData[index] == ',' && !leftBracketFlag)
                {
                    try
                    {
                        if (charData[index + 1] != '"') 
                        {
                            charData[index] = ' '; // change , to ' '
                            charData.Insert(index + 1, '['); // insert [ after ' '
                            leftBracketFlag = true;
                            commaFlag = true;
                            rightBracketFlag = false;
                        }
                        else 
                        {
                            charData[index] = ' '; // change , to ' '
                            rightBracketFlag = false;
                        }
                    } catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }
                else if (charData[index] == ',' && leftBracketFlag && commaFlag)
                {
                    charData[index] = ']'; // change ',' to ']'
                    charData.Insert(index + 1, ' '); // insert ' ' after ']'
                    rightBracketFlag = false;
                    leftBracketFlag = false;
                    commaFlag = false;
                }
                else if (charData[index] == '\n' || charData[index] == '\r')
                {
                    leftBracketFlag = false;
                    rightBracketFlag = false;
                    commaFlag = false;
                }
            }
            return new string(charData.ToArray());
        }
    }
}
