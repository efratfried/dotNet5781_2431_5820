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
                            throw ("ERROR");
                        }
                        else
                        {//if there is only one side of the line.
                            int index = IndexOfLine(item.LineNum);
                            NewLine = new BusLine(item.End, item.Start, WantedLine);
                            Lines.Insert(index, NewLine);
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
                throw ("ERROR,the busStop doesnt exist.");
            }
            else
            {
                throw ("ERROR! There are no busStop exist.");
            }
        }
        public BusLine ShortToLong()
        {
            if(Lines.Any())
            {
                foreach(var line in Lines)
                {

                }
            }
        }


       public void MergeSort(BusLine arr, BusLine temp , int left, int right)
        {
            int mid = (right + left) / 2;

            if (mid > left) //more than one cell
                MergeSort(arr, temp, left, mid);

            if (right > mid + 1)    //more than one cell
                MergeSort(arr, temp, mid + 1, right);

            Merge(arr, temp, left, mid, right);
        }

        void sort(int arr [])
        {
            int temp[ARR_LENGTH];   //array aid

            MergeSort(arr, temp, 0, ARR_LENGTH - 1);
        }

        int main()
        {
            srand((unsigned int)(time(NULL)));  //init the rand

            int arr[ARR_LENGTH];

            for (int i = 0; i < ARR_LENGTH; i++)
                arr[i] = rand() % 100;  //insert data

            for (int i = 0; i < ARR_LENGTH; i++)
                cout << arr[i] << " ";      //print before sorting
            cout << endl << endl;

            sort(arr);

            for (int i = 0; i < ARR_LENGTH; i++)
                cout << arr[i] << " ";      //print after sorting
            cout << endl;

            return (EXIT_SUCCESS);
        }
    }
}