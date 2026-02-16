using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 弹射之刃卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为1点
    // 卡牌效果：随机对一个随从造成$1点伤害。重复此效果，直到某个随从死亡。
    class Sim_GVG_050 : SimTemplate //* 弹射之刃 Bouncing Blade
    // Deal $1 damage to a random minion. Repeat until a minion dies.
    // 随机对一个随从造成$1点伤害。重复此效果，直到某个随从死亡。 
    {
        // 重写卡牌打出时的效果方法，这是弹射之刃卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据是否是己方打出，计算实际造成的伤害值（考虑法术伤害加成）
            int dmg = (ownplay) ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);

            // 创建随机数生成器
            Random rand = new Random();

            // 持续造成伤害直到有随从死亡
            while (p.ownMinions.Count > 0 || p.enemyMinions.Count > 0)
            {
                // 收集所有存活的随从
                List<Minion> allMinions = new List<Minion>();
                allMinions.AddRange(p.ownMinions);
                allMinions.AddRange(p.enemyMinions);

                // 如果没有存活的随从，则退出循环
                if (allMinions.Count == 0) break;

                // 随机选择一个随从
                Minion randomMinion = allMinions[rand.Next(allMinions.Count)];

                // 对选中的随从造成伤害
                p.minionGetDamageOrHeal(randomMinion, dmg);

                // 检查是否有随从死亡
                bool anyMinionDied = false;
                foreach (Minion m in p.ownMinions)
                {
                    if (m.Hp <= 0)
                    {
                        anyMinionDied = true;
                        break;
                    }
                }
                if (!anyMinionDied)
                {
                    foreach (Minion m in p.enemyMinions)
                    {
                        if (m.Hp <= 0)
                        {
                            anyMinionDied = true;
                            break;
                        }
                    }
                }

                // 如果有随从死亡，则停止弹射
                if (anyMinionDied) break;
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // REQ_MINIMUM_TOTAL_MINIONS - 场上至少需要有1个随从才能打出此卡牌
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_TOTAL_MINIONS, 1),
            };
        }
    }
}