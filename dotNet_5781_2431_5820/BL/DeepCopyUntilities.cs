﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    static class DeepCopyUtilities
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
        /*public static BO.StudentCourse CopyToStudentCourse(this DO.Station station, DO.BusStationLine sic)
        {
            BO.StudentCourse result = (BO.StudentCourse)course.CopyPropertiesToNew(typeof(BO.StudentCourse));
            // propertys' names changed? copy them here...
            result.Grade = sic.Grade;
            return result;
        }*/

        public static BO.Accident CopyToAccident(this DO.Bus Bus, DO.Accident sic)
        {
            BO.Accident result = (BO.Accident)Bus.CopyPropertiesToNew(typeof(BO.Accident));
            // propertys' names changed? copy them here...
            result.AccidentNum = sic.AccidentNum;
            return result;
        }
    }
}
