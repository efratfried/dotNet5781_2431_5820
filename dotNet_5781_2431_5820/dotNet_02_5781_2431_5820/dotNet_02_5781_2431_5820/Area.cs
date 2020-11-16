using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_5781_2431_5820
{
    namespace EnumExtension

    {
        public static class Extensions
        {
            public static Location Start;
            public static Location End;
            public Zone()
            public static Area GetArea(Area Zone)
            {
                switch (Zone)
                {
                    case Area.Jerusalem:
                        Zone=new Zone(35.2642)
                        break;
                    case Area.North:
                        break;

                    case Area.South:
                        break;

                    case Area.Center:
                        break;

                    case Area.Haifa:
                        break;

                    case Area.Tlv:
                        break;

                    case Area.General:
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        Console.WriteLine("ARE YOU CRAZY?!");
                        break;
                }
                return Zone;
            }
        }
        
    }
    public enum Area { Jerusalem, North, South, Center, Haifa, Tlv, General }

}
