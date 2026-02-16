using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 死亡领主卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值8
    // 卡牌效果：<b>嘲讽，亡语：</b>你的对手将一个随从从其牌库置入战场。
    class Sim_FP1_009 : SimTemplate //* 死亡领主 Deathlord
    // <b>Taunt. Deathrattle:</b> Your opponent puts a minion from their deck into the battlefield.
    // <b>嘲讽，亡语：</b>你的对手将一个随从从其牌库置入战场。 
    {
        // 定义要召唤的随从卡牌（这里以酸喉为例）
        CardDB.Card minion = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_612);

        // 重写亡语效果方法，这是死亡领主卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 确定召唤位置（在战场末尾）
            int summonPosition = (m.own) ? p.enemyMinions.Count : p.ownMinions.Count;

            // 为对手召唤一个随从
            // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤（这里为!m.own，即对手召唤），- false表示不是衍生物
            p.callKid(minion, summonPosition, !m.own, false);
        }
    }
}