using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    internal class Student : Human
    {
        public int StudentNumber;
        public float AverageMark;
        private string[] _seminarsTaken = new string[3]
        {
            "Mathatics",
            "Kinematics",
            "Dynamics"
        };

        public bool IsEligibleToEnroll()
        {
            return AverageMark >= 3.5f;
        }

        public string[] GetSeminarsTaken()
        {
            return _seminarsTaken;
        }

        public override void Breath()
        {
            Console.WriteLine($"학생이 숨쉰다");
        }

        public override void PurchaseParkingPass()
        {
            Console.WriteLine($"{Name} 이 주차권을 구매 했습니다. 학생할인 20% 적용 !!");
        }
    }
}
