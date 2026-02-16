using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 飞掠召唤英雄技能卡牌的模拟实现类，继承自SimTemplate基类
    // 这是纳克萨玛斯冒险模式中阿努布雷坎的英雄技能，费用为2点
    // 卡牌效果：<b>英雄技能</b>召唤一个4/4的蛛魔。
    class Sim_NAX1h_04 : SimTemplate //* 飞掠召唤 Skitter
    // <b>Hero Power</b>Summon a 4/4 Nerubian.
    // <b>英雄技能</b>召唤一个4/4的蛛魔。 
    {
        // 定义要召唤的蛛魔卡牌
        CardDB.Card nerubian = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.NAX1h_03);

        // 重写卡牌使用效果方法，这是飞掠召唤英雄技能的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 确定召唤位置（在己方或敌方战场末尾）
            int summonPosition = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

            // 召唤4/4的蛛魔
            // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤，- false表示不是衍生物
            p.callKid(nerubian, summonPosition, ownplay, false);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // 1. REQ_NUM_MINION_SLOTS - 至少需要一个随从位置
            // 这意味着场上必须有至少一个随从位置才能使用这个英雄技能
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
    }
}