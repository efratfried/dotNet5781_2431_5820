﻿using System;
using System.Security.Cryptography;
using System.Text;
using BO;

namespace BL
{
    public class BL : BlApi.IBL
    {
        DalApi.IDAL dal;
        public BL()
        {
            dal = DAL.DAL.Instance;
        }

        public DataPair GetDataPair(int n1, Func<int, int> second)
        {
            DataPair pair = new DataPair() { First = n1, FirstName = dal.GetData(n1).Name };
            pair.Second = second(n1);
            pair.SecondName = dal.GetData(pair.Second).Name;
            pair.Code = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(pair.FirstName + pair.SecondName));
            return pair;
        }
    }
}
