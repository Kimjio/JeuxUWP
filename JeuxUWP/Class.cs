using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeux
{
    class Class
    {
        private int count;
        private int endHour;
        private int endMinute;
        private int startHour;
        private int startMinute;
        public Class(int startHour, int endHour, int endMinute)
        {
            this.startHour = startHour;
            this.endMinute = endMinute;
            this.endHour = endHour;
          
        }

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        public int EndHour
        {
            get
            {
                return endHour;
            }

            set
            {
                endHour = value;
            }
        }

        public int EndMinute
        {
            get
            {
                return endMinute;
            }

            set
            {
                endMinute = value;
            }
        }

        public int StartHour
        {
            get
            {
                return startHour;
            }

            set
            {
                startHour = value;
            }
        }

        public int StartMinute
        {
            get
            {
                return startMinute;
            }

            set
            {
                startMinute = value;
            }
        }
        public int compare(int nowHour)
        {
            if (nowHour <= endHour && nowHour >= startHour)
            {
                //Console.WriteLine("실행됨~");
                if (startHour < 4)
                {
                    return startHour + 4;
                }else if (startHour==9)
                {
                    return endHour - 7;
                }else
                    return endHour - 8;
            }
            else return -1;
        }
    }
}
