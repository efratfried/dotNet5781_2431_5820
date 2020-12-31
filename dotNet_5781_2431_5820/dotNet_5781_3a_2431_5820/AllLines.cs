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
        public AllLines ()//ctor
        {
            Lines=new List<BusLine> ();
            busStops=new List<BusStopLine> ();
        }
        static Random rand = new Random();
        public void AddLine(int WantedLine)
        {
            BusLine NewLine;
           
            if (busStops.Count < 2)
            {
                throw new Exception("Add more stations");
            }
            else
            {
                if (!Lines.Any())
                {//if there arent any lines in the list i can add a new line.
                    int a = busStops.Count;
                    int randFirst = rand.Next(0, a);
                    int randLast = rand.Next(0, a);                  
                        while(randFirst.Equals(randLast))
                        {
                            randLast = rand.Next(0, a);
                        }
                    NewLine = new BusLine(busStops[randFirst], busStops[randLast], WantedLine);
                    Lines.Add(NewLine);
                }
                else
                {//if there are any lines we need to check if it is in the list.
                    foreach (var item in Lines)
                    {
                        if (WantedLine == item.LineNum)
                        {//if we found at least one line similar to the wanted line we need to check if they have different side.
                            if (item.OtherSide(this) != null)
                            {//if there are already two sides there is no option to add the wanted line.
                                throw new Exception("ERROR");
                            }
                            else
                            {//if there is only one side of the line.
                                int index = IndexOfLine(item.LineNum);
                                NewLine = new BusLine(item.End, item.Start, WantedLine);                          
                            }
                        }
                    }
                    //if not returned must be that the line isnt there.
                    if (IndexOfLine(WantedLine) == -1)
                    { //the list isnt empty but the wanted line isnt there.
                        int a = busStops.Count;
                        int randFirst = rand.Next(0, a);
                        int randLast = rand.Next(0, a);
                        while (randFirst.Equals(randLast))
                        {
                            randLast = rand.Next(0, a);
                        }
                        NewLine = new BusLine(busStops[randFirst], busStops[randLast], WantedLine);
                    }
                    else
                        throw new Exception("unknown");

                    //adding more stations to the line randomly.
                    int MaxAmountOfStations = rand.Next(0, busStops.Count);
                    for (int i = rand.Next(0, busStops.Count); i < MaxAmountOfStations; i+= rand.Next(0, 10))//almost totaly random
                    {
                        NewLine.AddStop(busStops[i]);
                    }
                    Lines.Add(NewLine);
                }
            }
        }
        public void RemoveLine(int removable)
        {
            if (!Lines.Any())
            {//if the list is empty.
                throw new Exception("there are no lines to delete");
            }
            else
            {
                int index = IndexOfLine(removable);
                if (index == -1)
                {//if the line does'nt exist in the list
                    throw new Exception("the line is not in the list");
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
        public string LinesInStop(int StationCode)
        {//get the num of a station & returns all the lines that passing by.
            if (busStops.Any())
            {//if the list has any arguments in it.
                string BusPath = "";
                foreach (var item in busStops)
                {//pass on all the bus stop list.
                    if (item.CodeStation == StationCode)
                    {
                        foreach (var line in Lines)
                        {//passing on all the lines list
                            if (line.StopOnLine(item))
                            {
                                BusPath += item.ToString();
                            }
                        }
                        return BusPath;
                    }
                }
                throw new Exception("ERROR,the busStop doesnt exist.");
            }
            else
            {
                throw new Exception("ERROR! There are no busStop exist.");
            }
        }
        public List<BusLine> ShortToLong()
        {
            if (Lines.Any())
            {
                //here we need to send to the sort function the line to do sort merge.
                return this.Sort(Lines, 0, Lines.Count);
            }
            else
            {
                throw new Exception("There are no lines yet!");

            }
        }
        public List<BusLine> Sort(List<BusLine> Lines, int Left, int Right)
        {//Merge sort using the function Merge
            if (Left < Right)
            {
                // Find the middle point
                int Middle = (Left + Right) / 2;

                // Sort first and second halves
                Sort(Lines, Left, Middle);
                Sort(Lines, Middle + 1, Right);

                // Merge the sorted halves
                Merge(Lines, Left, Middle, Right);
                return Lines;
            }
            else
            {
                return null; //check run time compilation
            }
        }
        public void Merge(List<BusLine> Arr, int Left, int Middle, int Right)
        {//merge two arrays

            int Arry1Lengh = Middle - Left + 1;
            int Arry2Lengh = Right - Middle;

            // Create temp arrays
            BusLine[] L = new BusLine[Arry1Lengh];
            BusLine[] R = new BusLine[Arry2Lengh];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < Arry1Lengh; ++i)
            {
                L[i] = Arr[Left + i];
            }
            for (j = 0; j < Arry2Lengh; ++j)
            {
                R[j] = Arr[Middle + 1 + j];
            }

            // Merge the temp arrays
            // Initial indexes of first and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged subarry array
            int k = Left;
            while (i < Arry1Lengh && j < Arry2Lengh)
            { //the driving time depended on the distance, so the comaration we did is about the distance & not about the actual time
                //we felt that it's not necessary to build another function when the time isnt acuorate & depended only on the distance.
                ///******(it will give the same result.)*******
                double l1 = L[i].DistanceBetweenTwoStations(L[i].Start.CodeStation, L[i].End.CodeStation);
                double l2 = R[j].DistanceBetweenTwoStations(R[j].Start.CodeStation, R[j].End.CodeStation);

                if (l1 <= l2)
                {
                    Arr[k] = L[i];
                    i++;
                }
                else
                {
                    Arr[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements of L[] if any
            while (i < Arry1Lengh)
            {
                Arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements of R[] if any
            while (j < Arry2Lengh)
            {
                Arr[k] = R[j];
                j++;
                k++;
            }
        }
        public BusLine this[int busLineNum]//indexer
        {
            get
            {
                return this.Lines.Find(item => item.LineNum == busLineNum);
            }
            set
            {}
        }
        public void AddStopToList()
        {
            BusStopLine a = new BusStopLine(false);
            busStops.Add(a);
        }
        public void RemoveStop(int UselessStop)
        {
            busStops.Remove(SearchStop(UselessStop));
        }
        public BusStopLine SearchStop(int CodeStop)
        {
            if (!busStops.Any())
            {//if the list is empty
                new Exception("The are'nt stops in the list");
                return null;
            }
            else
            {
                foreach (var item in busStops)
                {
                    if (item.CodeStation == CodeStop)
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }
}   
