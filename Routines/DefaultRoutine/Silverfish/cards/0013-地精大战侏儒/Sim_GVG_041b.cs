using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 大自然的防线卡牌的模拟实现类，继承自SimTemplate基类
    // 这是黑暗私语的抉择选项之一，法师职业法术卡牌，费用为0点
    // 卡牌效果：召唤5个小精灵。
    class Sim_GVG_041b : SimTemplate //* 大自然的防线 Nature's Defense
    // Summon 5 Wisps.
    // 召唤5个小精灵。 
    {
        // 在类级别定义小精灵卡牌对象，用于召唤
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_231);

        // 重写卡牌打出时的效果方法，这是大自然的防线卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 循环5次召唤小精灵
            for (int i = 0; i < 5; i++)
            {
                // 确定召唤位置：己方随从数量或敌方随从数量
                int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

                // 召唤小精灵到指定位置
                p.callKid(kid, pos, ownplay);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_NUM_MINION_SLOTS - 需要至少1个随从位置才能打出此卡牌
            // 2. REQ_MINION_TARGET - 目标必须是一个随从（虽然这张卡不需要目标，但可能是为了兼容性）
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}