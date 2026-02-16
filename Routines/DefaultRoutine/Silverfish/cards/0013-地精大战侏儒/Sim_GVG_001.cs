using System; // 引入.NET基础系统命名空间
using System.Collections.Generic; // 引入泛型集合相关类
using System.Text; // 引入文本处理相关类

namespace HREngine.Bots // 定义HREngine.Bots命名空间
{
    // 烈焰轰击卡牌的模拟实现类，继承自SimTemplate基类
    class Sim_GVG_001 : SimTemplate //* 烈焰轰击 Flamecannon
    //Deal $4 damage to a random enemy minion.
    //随机对一个敌方随从造成$4 点伤害。 
    {
        // 重写卡牌打出时的效果方法
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据是否是己方打出，确定目标随从列表（己方打出则目标为敌方随从，敌方打出则目标为己方随从）
            List<Minion> temp = (ownplay) ? p.enemyMinions : p.ownMinions;

            // 根据是否是己方打出，计算实际造成的伤害值（考虑法术伤害加成）
            int times = (ownplay) ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);

            // 检查目标随从列表是否至少有一个随从
            if (temp.Count >= 1)
            {
                // 创建随机数生成器实例
                Random rand = new Random();

                // 从目标随从列表中随机选择一个随从作为攻击目标
                Minion randomTarget = temp[rand.Next(temp.Count)];

                // 对随机选择的目标随从造成指定伤害
                p.minionGetDamageOrHeal(randomTarget, times);
            }
        }

        // 重写获取卡牌打出条件的方法
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，这里要求至少有一个敌方随从
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_ENEMY_MINIONS, 1),
            };
        }
    }
}