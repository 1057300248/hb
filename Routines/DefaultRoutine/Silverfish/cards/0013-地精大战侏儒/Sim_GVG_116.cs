using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 瑟玛普拉格卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为9点，攻击力9，生命值7
    // 卡牌效果：每当一个敌方随从死亡，召唤一个麻风侏儒。
    class Sim_GVG_116 : SimTemplate //* 瑟玛普拉格 Mekgineer Thermaplugg
    // Whenever an enemy minion dies, summon a Leper Gnome.
    // 每当一个敌方随从死亡，召唤一个麻风侏儒。 
    {
        // 定义要召唤的麻风侏儒卡牌
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_029);

        // 重写随从死亡触发方法，这是瑟玛普拉格卡牌效果的核心实现
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            // 根据瑟玛普拉格的归属确定要监听的敌方随从死亡数量
            int enemyMinionsDied = (m.own) ? p.tempTrigger.enemyMinionsDied : p.tempTrigger.ownMinionsDied;

            // 如果没有敌方随从死亡，则返回
            if (enemyMinionsDied == 0) return;

            // 计算本次需要处理的敌方随从死亡数量
            int residual = (p.pID == m.pID) ? enemyMinionsDied - m.extraParam2 : enemyMinionsDied;

            // 更新随从的状态标识
            m.pID = p.pID;
            m.extraParam2 = enemyMinionsDied;

            // 根据死亡的敌方随从数量召唤相应数量的麻风侏儒
            for (int i = 0; i < residual; i++)
            {
                // 在瑟玛普拉格的位置召唤麻风侏儒
                // 参数说明：- 要召唤的卡牌，- 召唤位置（瑟玛普拉格的位置），- 是否为己方召唤
                p.callKid(kid, m.zonepos, m.own);
            }
        }
    }
}