using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 克尔苏加德卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为8点，攻击力6，生命值8
    // 卡牌效果：在每个回合结束时，召唤所有在本回合中死亡的友方随从。
    class Sim_FP1_013 : SimTemplate //* 克尔苏加德 Kel'Thuzad
    // At the end of each turn, summon all friendly minions that died this turn.
    // 在每个回合结束时，召唤所有在本回合中死亡的友方随从。 
    {
        // 重写回合结束触发方法，这是克尔苏加德卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 遍历本回合死亡的随从
            foreach (GraveYardItem gyi in p.diedMinions.ToArray())
            {
                // 检查死亡随从是否属于克尔苏加德的拥有者
                if (gyi.own == triggerEffectMinion.own)
                {
                    // 获取死亡随从的卡牌数据
                    CardDB.Card card = CardDB.Instance.getCardDataFromID(gyi.cardid);

                    // 确定召唤位置（在战场末尾）
                    int pos = triggerEffectMinion.own ? p.ownMinions.Count : p.enemyMinions.Count;

                    // 召唤死亡的友方随从
                    // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤
                    p.callKid(card, pos, gyi.own);
                }
            }
        }
    }
}