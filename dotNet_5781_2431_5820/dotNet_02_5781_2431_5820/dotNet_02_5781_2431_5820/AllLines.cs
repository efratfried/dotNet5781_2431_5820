//efrat fried
//tamar packter

//we didnt have much time therefor we didnt finish to do all the request program , that is the best we could
//we hope so you appriciate the alot thinking & down to the smallest details.
using dotNet_02_5781_2431_5820.git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;



namespace dotNet_02_5781_2431_5820
{
    public class AllLines
    {
        public List<BusLine> Lines;
        public List<BusStopLine> busStops;
        public void AddLine(BusLine WantedLine)
        {
            if (!Lines.Any())
            {
                Lines.Add(WantedLine);
            }
            else
            {
                if (Lines.Contains(WantedLine))//check if the condition is not always false because of the adress ect...
                {
                    foreach (var item in Lines)
                    {
                        if(WantedLine.LineNum==item.LineNum)
                        {
                           if( item.OtherSide(this)!=null)
                            {
                                throw "ERROR";
                            }
                            else
                            {
                                WantedLine.End = item.Start;
                                WantedLine.Start = item.End;
                                Lines.Add(WantedLine); 
                            }
                        }
                    }
                }
            }
        }   
    }
}