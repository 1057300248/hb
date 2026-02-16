using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 连环爆裂卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为2点
    // 卡牌效果：造成$3到$6点伤害，<b>过载：</b>（1）
    class Sim_GVG_038 : SimTemplate //* 连环爆裂 Crackle
    // Deal $3-$6 damage. <b>Overload:</b> (1)
    // 造成$3到$6点伤害，<b>过载：</b>（1） 
    {
        // 重写卡牌打出时的效果方法，这是连环爆裂卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查目标是否有效（不为null）
            if (target != null)
            {
                // 创建随机数生成器
                Random rand = new Random();

                // 生成3到6之间的随机伤害值
                int randomDamage = rand.Next(3, 7); // Next(3, 7)生成3,4,5,6中的一个数

                // 根据是否是己方打出，计算实际造成的伤害值（考虑法术伤害加成）
                int dmg = (ownplay) ? p.getSpellDamageDamage(randomDamage) : p.getEnemySpellDamageDamage(randomDamage);

                // 对目标造成指定伤害
                // 调用游戏场地的minionGetDamageOrHeal方法，传递正的伤害值表示造成伤害
                p.minionGetDamageOrHeal(target, dmg);

                // 如果是己方打出，则增加过载计数
                if (ownplay) p.ueberladung++;
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 这意味着必须指定一个角色（英雄或随从）作为伤害目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
    }
}