using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace DAL
{
    sealed class DAL : IDAL
    {
        #region Singleton
        static readonly DAL instance = new DAL();
        static DAL() { }
        DAL() { }
        public static DAL Instance => instance;

        #endregion
        DS.DataSource ds = new DS.DataSource();
        public int GetDataCount() => ds.datas.Count();
        public Data GetData(int id)
        {
            return ds.datas[id].Clone();
        }
        public IEnumerable<Data> GetDatas()
        {
            List<Data> list = new List<Data>();
            foreach (var data in ds.datas)
                list.Add(data.Clone());
            return list;
            //
            //return from data in ds.datas
            //       select data.Clone();
        }
    }
}
