using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace DAL
{
    public sealed class DAL : IDAL
    {
        #region Singleton
        static readonly DAL instance = new DAL();
        static DAL() { }
        DAL() { }
        public static DAL Instance => instance;
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new DAL();
        //        return instance;
        //    }
        //}
        #endregion

        DS.DataSource ds = new DS.DataSource();
        public int GetDataCount() => ds.datas.Count();
        public Data GetData(int id) => ds.datas[id];
        public IEnumerable<Data> GetDatas() => ds.datas;
    }
}
