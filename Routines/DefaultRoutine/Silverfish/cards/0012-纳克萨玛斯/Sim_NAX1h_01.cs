using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 阿努布雷坎英雄模式卡牌的模拟实现类，继承自SimTemplate基类
    // 这是纳克萨玛斯冒险模式中的英雄，通常具有特殊效果
    class Sim_NAX1h_01 : SimTemplate //* 阿努布雷坎 Anub'Rekhan
    // 特殊英雄技能或效果（具体效果需参考纳克萨玛斯冒险模式）
    {
        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // 1. REQ_NUM_MINION_SLOTS - 至少需要一个随从位置
            // 这意味着场上必须有至少一个随从位置才能使用这张卡牌
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
    }
}