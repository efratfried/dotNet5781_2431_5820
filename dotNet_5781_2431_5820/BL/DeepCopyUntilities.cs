using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class DeepCopyUtilities
    {
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
        //public static BO.StudentCourse CopyToStudentCourse(this DO.Course course, DO.StudentInCourse sic)
        //{
        //    BO.StudentCourse result = (BO.StudentCourse)course.CopyPropertiesToNew(typeof(BO.StudentCourse));
        //    // propertys' names changed? copy them here...
        //    result.Grade = sic.Grade;
        //    return result;
        //}

        public static BO.BusLine CopyDOLineStationToBOLine(this DO.BusLine lineDO, DO.BusStationLine linestation)
        {
            BO.BusLine result = (BO.BusLine)lineDO.CopyPropertiesToNew(typeof(BO.BusLine));//copy the relevant properties of lineDO to a new object- lineBO

            //important!
            //we still didnt restart the result's "lineStations" list. for that, we need to reach the "listLineStations" in DataSource, wicth we cannot do here. 
            //we will do it to the result from BLImp, by using "dl" to call the func: GetLineStationsListOfALine.
            return result;
        }

        //public static BO.LineStation CopyDOLineStationToBOLineSttation(this DO.LineStation lineStationDO, DO.LineStation linestationBO)
        //{
        //    BO.LineStation result = (BO.LineStation)lineStationDO.CopyPropertiesToNew(typeof(BO.LineStation));//copy the relevant properties of lineStationDO to a new object- linestationBO

        //    //important!
        //    //we still didnt restart the result's Distance, Time and LineId fields. for that, we need to reach the "listStations" and "listAdjacentStations" in DataSource, wicth we cannot do here. 
        //    //we will do it to the result from BLImp, by using "dl".
        //    return result;
        //}


    }
}
