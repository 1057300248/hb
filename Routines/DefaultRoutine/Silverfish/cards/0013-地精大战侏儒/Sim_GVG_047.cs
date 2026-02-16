using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 暗中破坏卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为4点
    // 卡牌效果：随机消灭一个敌方随从，<b>连击：</b>并且摧毁你对手的武器。
    class Sim_GVG_047 : SimTemplate //* 暗中破坏 Sabotage
    // Destroy a random enemy minion. <b>Combo:</b> And your opponent's weapon.
    // 随机消灭一个敌方随从，<b>连击：</b>并且摧毁你对手的武器。 
    {
        // 重写卡牌打出时的效果方法，这是暗中破坏卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据是否是己方打出，确定要检查的随从列表
            List<Minion> temp = (ownplay) ? p.enemyMinions : p.ownMinions;

            // 如果存在随从，则随机选择一个消灭
            if (temp.Count >= 1)
            {
                // 创建随机数生成器
                Random rand = new Random();

                // 随机选择一个敌方随从
                Minion randomMinion = temp[rand.Next(temp.Count)];

                // 消灭选中的随从
                if (randomMinion != null)
                {
                    p.minionGetDestroyed(randomMinion);
                }
            }

            // 检查是否触发连击效果（本回合已打出至少一张卡牌）
            if (p.cardsPlayedThisTurn >= 1)
            {
                // 摧毁对手的武器（耐久度减少1000点，相当于完全摧毁）
                p.lowerWeaponDurability(1000, !ownplay);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 暗中破坏不需要目标，可以直接打出
            return new PlayReq[] { };
        }
    }
}