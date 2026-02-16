using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 恶魔之心卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为5点
    // 卡牌效果：对一个随从造成$5点伤害，如果该随从是友方恶魔，则改为使其获得+5/+5。
    class Sim_GVG_019 : SimTemplate //* 恶魔之心 Demonheart
    // Deal $5 damage to a minion.  If it's a friendly Demon, give it +5/+5 instead.
    // 对一个随从造成$5点伤害，如果该随从是友方恶魔，则改为使其获得+5/+5。 
    {
        // 重写卡牌打出时的效果方法，这是恶魔之心卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查目标是否为友方恶魔（即与施法者同属一方且种族为恶魔）
            if (target.own == ownplay && (TAG_RACE)target.handcard.card.race == TAG_RACE.DEMON)
            {
                // 如果是友方恶魔，则给予+5攻击力和+5生命值的增益效果
                // 调用游戏场地的minionGetBuffed方法来修改随从的属性
                p.minionGetBuffed(target, 5, 5);
            }
            else
            {
                // 如果不是友方恶魔，则造成伤害
                // 根据是否是己方打出，计算实际造成的伤害值（考虑法术伤害加成）
                int dmg = (ownplay) ? p.getSpellDamageDamage(5) : p.getEnemySpellDamageDamage(5);

                // 对目标造成指定伤害
                // 调用游戏场地的minionGetDamageOrHeal方法，传递正的伤害值表示造成伤害
                p.minionGetDamageOrHeal(target, dmg);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}