//efrat fried
//tamar packter
using dotNet_02_5781_2431_5820.git;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;

namespace dotNet_02_5781_2431_5820.git
{
    public class BusLine : IEnumerator
    {
        public int LineNum;
        public  BusStopLine Start { get; set; }
        public  BusStopLine End { get; set; }
        public List<BusStopLine> LineStops;//list of all the station of the line
        public Area MyArea;
        private int iCurrent = -1;
        public  BusLine OtherSide(ref AllLines MyLines)
        {
            foreach(BusLine l in MyLines.Lines)
            {
                if ((l.LineNum==LineNum)&& (l.Start==End))
                {
                    return  l;
                }                
            }
            return null;
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

       
        public bool MoveNext()
        {
            if (iCurrent < LineStops.Count - 1)
            {
                ++iCurrent;
                return true;
            }
            return false;
        }
        static public IEnumerable<T> Swap3<T>(this IList<T> source, int index1, int index2)
{
    // Parameter checking is skipped in this example.
    // It is assumed that index1 < index2. Otherwise a check should be build in and both indexes should be swapped.

    using (IEnumerator<T> e = source.GetEnumerator())
    {
        // Iterate to the first index.
        for (int i = 0; i < index1; i++)
            yield return source[i];

        // Return the item at the second index.
        yield return source[index2];

        if (index1 != index2)
        {
            // Return the items between the first and second index.
            for (int i = index1 + 1; i < index2; i++)
                yield return source[i];

            // Return the item at the first index.
            yield return source[index1];
        }

        // Return the remaining items.
        for (int i = index2 + 1; i < source.Count; i++)
            yield return source[i];
    }
}
        public void Reset()
        {
            iCurrent = -1;
        }
        public object Current
        {
            get
            {
                return LineStops[iCurrent];
            }
        }
        public BusLine(BusStopLine first, BusStopLine last, BusLine Side2=null)
        {
            checked
            {
                if(first==last)
                {
                    throw ("you need to enter two different stops");
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

        public void RemoveStop(BusStopLine UselessStop,AllLines  MyLines)
        {

            if (LineStops.Contains(UselessStop))//PROBLEM NEED TO DO FOR EACH FIRST ON USELESSSSTOP
            {
                if (UselessStop == Start || UselessStop == End)
                {
                    LineStops.Remove(UselessStop);
                    Start = LineStops[0];
                    End = LineStops[LineStops.Count];
                    if (OtherSide(ref MyLines) != null)
                    {
                        OtherSide(ref MyLines).RemoveStop(UselessStop, MyLines);
                        if(Start == OtherSide(ref MyLines).End)//it means we removed the end in this
                        {
                            //means otherside start not eaqual to org end
                            if (!OtherSide(ref MyLines).LineStops.Contains(End))
                            {//case doesnt exist
                               OtherSide(ref MyLines).AddStop(End,"start");
                            }
                            else
                            {
                                foreach (BusStopLine l in LineStops)
                                {
                                    if(l==End)
                                    {
                                        LineStops.;
                                    }
                                }
                            }
                        }
                        //means otherside end not eaqual to org start
                        else
                        {

                        }
                    }
                    //we need to take care of when there are 2 sides to the line!!!!!!!!!!!!!!!!!!!!!!!!!!!
                }
            }
            else
            {
                throw ("could'nt find the requested station");
            }
        }
       public void AddStop(BusStopLine NewStop,string state=null)
        {
          int index=  WhereToAdd(NewStop,state);
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
        public int  WhereToAdd(BusStopLine NewStop,string state=null)//nedd to take care of other side!!!!!!!
        {
            int i=0;
            int k;
            if(state=="start")
                {
                return 0;
                }
            else if(state=="end")
            {
                return LineStops.Count;
            }
            double FirstDis=LineStops[i].DistancefromPriviouStation(NewStop,LineStops[i++]);
            double LastDis = LineStops[LineStops.Count].DistancefromPriviouStation(NewStop, LineStops[LineStops.Count]);
            if()
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
                int index1 = -1;
                int index2 = -1;
                for (int i = 0; i < LineStops.Count; i++)
                {//find the location of the first station to start the sub path.
                    if (LineStops[i].CodeStation == station1.CodeStation)
                    {
                        index1 = i;
                    }
                }
                if (index1 == -1)  //in that case it n=meens that the wanted codestation isnt exsist.
                {
                    throw ("ERROR");
                }
                for (int i = 0; i < LineStops.Count; i++)
                {//find the location of the second station to end the sub path.
                    if (LineStops[i].CodeStation == station2.CodeStation)
                    {
                        index2 = i;
                    }
                }
                if (index2 == -1)  //in that case it n=meens that the wanted codestation isnt exsist.
                {
                    throw ("ERROR");

                }

                int temp = index1;
                index1 = Math.Min(index1, index2);
                index2 = Math.Max(temp, index2);
                BusLine SubPath = new BusLine(LineStops[index1],LineStops[index2]);
               
                for (int i = ++index1, j = index2; i < j; i++)
                {//the loop goes from the index of the first sub station to the second one.
                    SubPath.LineStops.Insert(i,LineStops[i]);
                }
                return SubPath;
             }
             else
            {
                throw ("could'nt find the wanted stations in the line");
            }   
        }
        public override string ToString()
        {//returns in this format: busline:122 area:jerusalem one side: 115478 222555 second side: 222555 115478
            string ThisSidePath = "first side: ";
            ThisSidePath += ReturnsPathString(this);
            string OtherSidePath="second side: ";
            if(OtherSide[0]!=null)
            {
                OtherSidePath+= ReturnsPathString(OtherSide[0]);
            }
            else
            {
                OtherSidePath = ".";
            }
            return "busline:"+LineNum+"area:"+MyArea+ ThisSidePath+ OtherSidePath;
        }
       public string ReturnsPathString(BusLine WantedBusLine)
        {//returns string with the path of the bus that was sent to her
            string OtherSidePath = " ";
                for (int i = 0; i <= WantedBusLine.LineStops.Count; i++)
                {
                    OtherSidePath += WantedBusLine.LineStops[i].CodeStation;
                }
                return OtherSidePath;
        }

        public double DistanceBetweenTwoStations(int StationCode1, int StationCode2)
        {//function to get the distance between two stations.
            int FirstStationIndex = IndexOfStation(StationCode1);
            double TotalDistance=0;
            int SecondStationIndex= IndexOfStation(StationCode2);
            int temp;
            for (; FirstStationIndex < SecondStationIndex; FirstStationIndex++)
            {
                TotalDistance += this.LineStops[FirstStationIndex].DistancefromPriviouStation(LineStops[FirstStationIndex],LineStops[FirstStationIndex + 1]);
            }
            return TotalDistance;
        }
        public int IndexOfStation(int StationCode)
        {//return the index of the station in the array
            foreach(BusStopLine L in LineStops)
            {
                if(L.CodeStation== StationCode)//if the busline is in the "array" return index
                {
                    return LineStops.IndexOf(L);
                }
            }
            return -1;//if the busline wasnt found.
        }
    }
};