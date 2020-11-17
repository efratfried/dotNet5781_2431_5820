﻿using System.Collections.Generic;

namespace dotNet_02_5781_2431_5820.git
{
    internal class BusLine
    {
      public  int LineNum;
      public static BusStopLine Start { get; set; }
      public  static BusStopLine End { get; set; }
        public List <BusStopLine> LineStops;//list of all the station of the line
        public Area MyArea;
        public BusLine(BusStopLine first, BusStopLine last)
        {
            checked
            {
                if(first==last)
                {
                    throw ("you need to enter to different stops");
                }
                else
                {
                    Start = first;
                    End = last;
                    LineStops.Add(first);
                    LineStops.Add(last);
                }
            }
        }

        public void RemoveStop(BusStopLine UselessStop)
        {

            if (LineStops.Contains(UselessStop))
            {
                if (UselessStop == LineStops[0] || UselessStop == LineStops[LineStops.Count])
                {
                    //we need to take care of when there are 2 sides to the line!!!!!!!!!!!!!!!!!!!!!!!!!!!
                }

                LineStops.Remove(UselessStop);
            }
            else
            {
                throw ("could'nt find the requested station");
            }
        }
       public void AddStop(BusStopLine NewStop)
        {
          int index=  WhereToAdd(NewStop);
            if (index == 0 || index == LineStops.Count)
            {
                // remeber!!!!! we need to take care of when you have two sides line we need to change his last/first opsite one too!!!!!!!:)
                if (index==0)
                {
                    Start = NewStop;
                }
                else
                {
                    End = NewStop;
                }
            }
                LineStops.Insert(index, NewStop);
        }
        public int  WhereToAdd(BusStopLine NewStop)
        {
            int i=0;
            int k;
            double FirstDis=LineStops[i].DistancefromPriviouStation(NewStop,LineStops[i++]);
            double LastDis = LineStops[LineStops.Count].DistancefromPriviouStation(NewStop, LineStops[LineStops.Count]);

            for ( k=i-1;i<=LineStops.Count;i++,k++)
               {
                if((LineStops[i].DistancefromPriviouStation(NewStop, LineStops[i])< (LineStops[i].DistancefromPriviouStation(LineStops[i], LineStops[k]))&& (LineStops[i].DistancefromPriviouStation(NewStop, LineStops[k]) < LineStops[i].DistancefromPriviouStation(LineStops[i], LineStops[k]))))
                    {
                    return k;//the first one that it shorten its way
                    }
               }
            //if you came here it means that no one shortage his path so he needs to be added to first or last stop
                if (FirstDis>LastDis)
            {
                return LineStops.Count;
            }
            else
            {
                return 0;
            }
        }

        public bool ValidLineNum(int Numl)//if the line num is bigger than 3 digits
        {
            if ((Numl >= 1)&&(Numl<=999))
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
        public bool StopOnLine(BusStopLine station)
        {
            //check if the station is on the line.
            return LineStops.Contains(station);
        }

        public BusLine SubPath(BusStopLine station1 , BusStopLine station2)
        {//get 2 stations & return all the stations between them in a new line.
             if (StopOnLine(station1)&& StopOnLine(station2))
            {
                LineStops.GetRange(LineStops.(station1))      //  *********************continue fron here*********************************
            }
             else
            {
                throw ("could'nt find the wanted stations in the line");
            }
          
        }
    }
}