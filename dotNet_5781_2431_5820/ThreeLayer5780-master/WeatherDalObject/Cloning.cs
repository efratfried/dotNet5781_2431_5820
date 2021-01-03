using System;
using DO;

namespace Dal
{
    static class Cloning
    {
        internal static IClonable Clone(this IClonable original)
        {
            IClonable target = (IClonable)Activator.CreateInstance(original.GetType());
            //...
            return target;
        }

        internal static T Clone<T>(this T original)
        {
            T target = (T)Activator.CreateInstance(original.GetType());
            //...
            return target;
        }

        internal static WindDirection Clone(this WindDirection original)
        {
            WindDirection target = new WindDirection();
            target.direction = original.direction;
            return target;
        }
    }
}
