using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 空灵召唤者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值4
    // 卡牌效果：<b>亡语：</b>随机将一张恶魔牌从你的手牌置入战场。
    class Sim_FP1_022 : SimTemplate //* 空灵召唤者 Voidcaller
    // <b>Deathrattle:</b> Put a random Demon from your hand into the battlefield.
    // <b>亡语：</b>随机将一张恶魔牌从你的手牌置入战场。 
    {
        // 定义默认恶魔卡牌（作为敌方情况的后备）
        CardDB.Card defaultDemon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_301);

        // 重写亡语效果方法，这是空灵召唤者卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 检查空灵召唤者是否属于己方
            if (m.own)
            {
                // 收集手牌中的所有恶魔卡牌
                List<Handmanager.Handcard> demonCards = new List<Handmanager.Handcard>();
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if ((TAG_RACE)hc.card.race == TAG_RACE.DEMON)
                    {
                        demonCards.Add(hc);
                    }
                }

                // 如果有恶魔卡牌在手牌中
                if (demonCards.Count > 0)
                {
                    // 创建随机数生成器
                    Random rand = new Random();

                    // 随机选择一张恶魔卡牌
                    Handmanager.Handcard selectedDemon = demonCards[rand.Next(demonCards.Count)];

                    // 在战场末尾召唤选中的恶魔
                    p.callKid(selectedDemon.card, p.ownMinions.Count, true, false);

                    // 从手牌中移除该恶魔卡牌
                    p.removeCard(selectedDemon);
                }
            }
            else
            {
                // 敌方空灵召唤者死亡时的处理
                if (p.enemyAnzCards >= 1)
                {
                    // 召唤默认恶魔（实际游戏中应该也是随机选择敌方手牌中的恶魔）
                    p.callKid(defaultDemon, p.enemyMinions.Count, false, false);
                }
            }
        }
    }
}