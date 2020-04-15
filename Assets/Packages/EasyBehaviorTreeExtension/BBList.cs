using System.Collections;
using System.Collections.Generic;

namespace AillieoUtils.EasyBehaviorTree
{
    public struct BBList<T> : IBlackBoardData
    {
        public List<T> list;

        public BBList(List<T> values)
        {
            list = values;
        }
    }
}