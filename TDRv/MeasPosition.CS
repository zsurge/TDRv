using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    public static class MeasPosition
    {
        //这里来定义是否已经开路，并记录下开路的值
        private static bool isopen = false;

        //差分
        private static int tdd1indexvalue = 0;

        //单端
        private static int tdd2Indexvalue = 0;

        private static int tdd1startPosition = 0;
        private static int tdd2startPosition = 0;

        public static bool isOpen
        {
            get { return isopen; }
            set { isopen = value; }
        }
        public static int tdd11IndexValue
        {
            get { return tdd1indexvalue; }
            set { tdd1indexvalue = value; }
        }
        public static int tdd22IndexValue
        {
            get { return tdd2Indexvalue; }
            set { tdd2Indexvalue = value; }
        }
        public static int tdd11start
        {
            get { return tdd1startPosition; }
            set { tdd1startPosition = value; }
        }
        public static int tdd22start
        {
            get { return tdd2startPosition; }
            set { tdd2startPosition = value; }
        }
    }
}
