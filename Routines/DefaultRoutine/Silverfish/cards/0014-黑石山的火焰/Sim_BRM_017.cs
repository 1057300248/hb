using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 复活术卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张牧师职业法术卡牌，费用为2点
    // 卡牌效果：随机召唤一个在本局对战中死亡的友方随从。
    class Sim_BRM_017 : SimTemplate //* 复活术 Resurrect
    // Summon a random friendly minion that died this game.
    // 随机召唤一个在本局对战中死亡的友方随从。 
    {
        // 重写卡牌使用效果方法，这是复活术卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 确定召唤位置
            int summonPosition = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;

            // 获取要召唤的最近死亡的友方随从
            CardDB.Card minionToSummon = CardDB.Instance.getCardDataFromID(p.OwnLastDiedMinion);

            // 召唤随从
            // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤，- false表示不是衍生物
            p.callKid(minionToSummon, summonPosition, ownplay, false);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_NUM_MINION_SLOTS - 至少需要一个随从位置
            // 2. REQ_FRIENDLY_MINION_DIED_THIS_GAME - 需要有友方随从在本局对战中死亡
            // 这意味着需要有空余位置且有可复活的随从
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_MINION_DIED_THIS_GAME),
            };
        }
    }
}