using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace spacewizards.Utilities
{
    public class FileInputHandler
    {
        public string FileName { get; set; }

        public FileInputHandler(Game1 game)
        {

        }

        public string[,] parseToArray(string path)
        {
            this.FileName = @"../../../../GameContent/Maps/" + path + ".csv";
            StreamReader sr = new StreamReader(FileName);
            String[,] strArray = null;
            int Row = 0;
            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                while (Row < Line.Length)
                {
                    if (Row == 0)
                    {
                        strArray = new String[Line.Length, Line.Length];
                    }
                    for (int column = 0; column < Line.Length; column++)
                    {
                        strArray[Row, column] = Line[column];
                    }
                    Row++;
                    if (!sr.EndOfStream)
                    {
                        Line = sr.ReadLine().Split(',');
                    }
                }
            }
            return strArray;
        }
    }
}
