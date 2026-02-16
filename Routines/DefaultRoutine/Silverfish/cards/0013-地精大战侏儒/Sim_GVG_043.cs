using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 重型刃弩卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业武器卡牌，费用为2点，攻击力2，耐久度2
    // 卡牌效果：<b>战吼：</b>随机使一个友方随从获得+1攻击力。
    class Sim_GVG_043 : SimTemplate //* 重型刃弩 Glaivezooka
    // <b>Battlecry:</b> Give a random friendly minion +1 Attack.
    // <b>战吼：</b>随机使一个友方随从获得+1攻击力。 
    {
        // 在类级别定义重型刃弩卡牌对象，用于装备武器
        CardDB.Card w = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_043);

        // 重写卡牌打出时的效果方法，这是重型刃弩卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备重型刃弩武器
            p.equipWeapon(w, ownplay);
        }

        // 重写战吼效果方法，这是重型刃弩卡牌战吼效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据是否是己方随从，确定要检查的随从列表
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;

            // 如果没有随从，则直接返回
            if (temp.Count <= 0) return;

            // 创建随机数生成器
            Random rand = new Random();

            // 随机选择一个友方随从
            Minion randomMinion = temp[rand.Next(temp.Count)];

            // 给选中的随从增加+1攻击力
            if (randomMinion != null)
            {
                p.minionGetBuffed(randomMinion, 1, 0);
            }
        }
    }
}