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
    public class BusLine : IComparable, IEnumerator
    {
        //FIELDS
        public int LineNum;
        public BusLine(BusStopLine first = null, BusStopLine last = null, int LineNUm = 0)
        {
            if(first==null)
            {
                Console.WriteLine("please enter the line num" );
                LineNum = Console.Read();
            }
            else
            {
                if (first == last)
                {//no circle lines.
                    throw new Exception("you need to enter two different stops");
                }
                else
                {
                    Start = first;
                    End = last;
                    LineStops = new List<BusStopLine>();
                    LineStops.Add(first);
                    LineStops.Add(last);
                    this.LineNum = LineNUm;
                }
            }              
        }
        public  BusStopLine Start { get; set; }
        public  BusStopLine End { get; set; }
        public List<BusStopLine> LineStops;//list of all the station of the line
        public Area MyArea;
        private int iCurrent = -1;
        public BusLine OtherSide(AllLines MyLines)
        {
            foreach(BusLine L in MyLines.Lines)
            {
                if ((L.LineNum == LineNum) && ((L.Start == End)||(L.End == Start)))
                {
                    return  L;
                }
            }
            return null;
        }              
        //FUNCTIONS
        public void RemoveStop(BusStopLine UselessStop,AllLines  MyLines)
        {
            if (LineStops.Contains(UselessStop))//PROBLEM NEED TO DO FOR EACH FIRST ON USELESSS STOP
            {
                LineStops.Remove(UselessStop);//check if not doing any problems.
                if (UselessStop == Start || UselessStop == End)
                {
                    Start = LineStops[0];
                    End = LineStops[LineStops.Count];
                    if (OtherSide( MyLines) != null)
                    {
                        OtherSide( MyLines).RemoveStop(UselessStop, MyLines);
                        if(Start == OtherSide( MyLines).End)//it means we removed the end in this
                        {
                            //means otherside start not eaqual to org end
                            if (!OtherSide( MyLines).LineStops.Contains(End))
                            {//case doesnt exist
                               OtherSide( MyLines).AddStop(End,"start");
                            }
                            else
                            {
                                foreach (BusStopLine l in OtherSide(MyLines).LineStops)
                                {
                                    if(l==End)
                                    {
                                        OtherSide(MyLines).LineStops.Remove(End);
                                        OtherSide(MyLines).LineStops.Insert(0, End);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //means otherside end not eaqual to org start
                            if (!OtherSide(MyLines).LineStops.Contains(Start))
                            {//case doesnt exist
                                OtherSide(MyLines).AddStop(Start, "End");
                            }
                            else
                            {
                                foreach (BusStopLine l in OtherSide(MyLines).LineStops)
                                {
                                    if (l == Start)
                                    {
                                        OtherSide(MyLines).LineStops.Remove(Start);
                                        OtherSide(MyLines).LineStops.Insert(LineStops.Count, Start);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception("could'nt find the requested station");
            }
        }
       public void AddStop(BusStopLine NewStop,string state=null)
        {
           if( LineStops.Contains(NewStop))
            {
               throw new Exception("The stop is already in the Line's stops list");
            }

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
                    throw new Exception ("ERROR");
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
                    throw new Exception("ERROR");

                }

                int temp = index1;
                index1 = Math.Min(index1, index2);
                index2 = Math.Max(temp, index2);
                BusLine SubPath = new BusLine(LineStops[index1],LineStops[index2],LineNum);
               
                for (int i = ++index1, j = index2; i < j; i++)
                {//the loop goes from the index of the first sub station to the second one.
                    SubPath.LineStops.Insert(i,LineStops[i]);
                }
                return SubPath;
             }
             else
            {
                throw new Exception("could'nt find the wanted stations in the line");
            }   
        }
        public override string ToString()
        {//returns in this format: busline:122 area:jerusalem one side: 115478 222555 second side: 222555 115478
            string ThisSidePath = "The line's path: ";
            ThisSidePath += ReturnsPathString(this);
            return "busline:"+LineNum+"area:"+MyArea+ ThisSidePath;
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
            for (; FirstStationIndex < SecondStationIndex; FirstStationIndex++)
            {
                TotalDistance += this.LineStops[FirstStationIndex].DistancefromPriviouStation(LineStops[FirstStationIndex],LineStops[FirstStationIndex + 1]);
            }
            return TotalDistance;
        }
        public TimeSpan DrivingTimeBetweenTwoStations(int StationCode1, int StationCode2)
        {
            TimeSpan TotalTime=new TimeSpan() ;
            int Before = IndexOfStation(StationCode1);
            int After= IndexOfStation(StationCode2);
            if(Math.Min(Before, After)== After)//there is an option that the min function return a boolian value.
            {//like the swap function that isnt exsist.
                int temp = Before;
                Before = After;
                After = temp;
            }

            for (; Before < After; Before++)//get the whole time 
            {
             TotalTime += this.LineStops[Before].TimefromPriviouStation( LineStops[Before + 1]);
            }
             return TotalTime;
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

        //the indexer for busline
        
        public int CompareTo(object obj)
        {//comparing distance from one stop to other between two lines and returns who is the realationship between them
            if (obj == null) return 1;//end of array
            BusLine otherline = obj as BusLine;
            double a_total_dis = 0;
            double b_total_dis = 0;
            int desStationNum;
            Console.WriteLine("enter destination station");
            desStationNum = Console.Read();      
            
            foreach (BusStopLine a in otherline.LineStops)
            {
                if (a.CodeStation != desStationNum && (iCurrent+1< otherline.LineStops.Count))
                {
                    a_total_dis += a.DistancefromPriviouStation(a, otherline.LineStops[iCurrent+1]);
                }
                else
                {
                    break;
                } 
            }
            foreach (BusStopLine b in this.LineStops)
            {
                if (b.CodeStation != desStationNum && (iCurrent + 1 < otherline.LineStops.Count))
                {
                    b_total_dis += b.DistancefromPriviouStation(b, otherline.LineStops[iCurrent + 1]);
                }
                else
                {
                    break;
                }
            }
            if(a_total_dis<b_total_dis)
            {
                return 1;
            }
            else if(a_total_dis > b_total_dis)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }//we need to do two sub pathes that starts in current station and end in destination
        public bool MoveNext()
        {
            if (iCurrent < LineStops.Count - 1)
            {
                ++iCurrent;
                return true;
            }
            return false;
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
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
};