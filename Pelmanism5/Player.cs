using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelmanism5
{
    internal class Player
    {
        private int nowOpenCardIndex1, nowOpenCardIndex2;
        public Player()
        {
            BeforeOpenCardIndex1 = BeforeOpenCardIndex2 = -1;
            NowOpenCardIndex1 = NowOpenCardIndex2 = -1;
            OpenCounter = 0;
        }

        public int OpenCounter {  get; set; }
        public int BeforeOpenCardIndex1 {  get; set; }

        public int BeforeOpenCardIndex2 { get; set; }

        public int NowOpenCardIndex1
        {
            get { return nowOpenCardIndex1; }
            set
            {
                nowOpenCardIndex1 = value;
                OpenCounter++;
            }
        }

        public int NowOpenCardIndex2
        {
            get { return nowOpenCardIndex2; }
            set
            {
                nowOpenCardIndex2 = value;
                OpenCounter++;
            }
        }

        public void Reset()
        {
            BeforeOpenCardIndex1 = NowOpenCardIndex1;
            BeforeOpenCardIndex2 = NowOpenCardIndex2;
            NowOpenCardIndex1 = NowOpenCardIndex2 = -1;
            OpenCounter = 0;
        }

    }
}
