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
        public void AddLine(int WantedLine)
        {
            BusLine NewLine=null;
            Random rand = new Random();
            if (!Lines.Any())
            {//if there arent any line in the list i can add a new line.
                int a= busStops.Count;
                int randFirst = rand.Next(0,a);
                int randLast =rand.Next(0,a);
               NewLine = new BusLine(busStops[randFirst], busStops[randLast], WantedLine);
            }
            else
            {//if there are any lines we need to check if it is in the list.
                foreach (var item in Lines)
                {
                    if (WantedLine == item.LineNum)
                    {//if we found at least one line similar to the wanted line we need to check if they have different side.
                        if (item.OtherSide(this) != null)
                        {//if there are already two sides there is no option to add the wanted line.
                            throw "ERROR";
                        }
                        else
                        {//if there is only one side of the line
                            NewLine = new BusLine(item.End, item.Start, WantedLine);
                        }
                    }
                }
                //if not returned must be that the line isnt there.
               if(NewLine==null)
               { //the list isnt empty but the wanted line isnt there.
                   int a = busStops.Count;
                   int randFirst = rand.Next(0, a);
                   int randLast = rand.Next(0, a);
                   NewLine = new BusLine(busStops[randFirst], busStops[randLast], WantedLine);
               }
                Lines.Add(NewLine);
            }
        }
        public void RemoveLine(int removable)
        {
            if(!Lines.Any())
            {//if the list is empty.
                throw ("there are no lines to delete");
            }
            else
            {
                int index = IndexOfLine(removable);
                if (index==-1)
                {//if the line does'nt exist in the list
                    throw "the line is not in the list";
                }
                else
                {
                    Lines.Remove(Lines[index]);
                }
            }
        }
        public int IndexOfLine(int LineNum)
        {//return the index of the line in the array
        
            foreach (BusLine L in Lines)
            {
                if (L.LineNum == LineNum)//if the busline is in the "array" return index
                {
                    return Lines.IndexOf(L);
                }
            }
            return -1;//if the busline wasnt found.
        }
       public BusLine LinesInStop(int StationCode)
        {
            if(busStops.Any())
            {//if the list has any arguments in it
                for (int i = 0; i < busStops.Count; i++)
                {//pass on all the bus stop list
                    if(busStops[i].CodeStation==StationCode)
                    {
                        for (int j = 0; j < Lines.Count; j++)
                        {
                            if(Lines[j].)
                        }
                    }
                }
            }
        }
          
    }
}