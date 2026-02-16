using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 小鬼爆破卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为4点
    // 卡牌效果：对一个随从造成$2-$4点伤害。每造成1点伤害，便召唤一个1/1的小鬼。
    class Sim_GVG_045 : SimTemplate //* 小鬼爆破 Imp-losion
    // Deal $2-$4 damage to a minion. Summon a 1/1 Imp for each damage dealt.
    // 对一个随从造成$2-$4点伤害。每造成1点伤害，便召唤一个1/1的小鬼。 
    {
        // 在类级别定义小鬼卡牌对象，用于召唤
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_045t);

        // 重写卡牌打出时的效果方法，这是小鬼爆破卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 生成2到4之间的随机伤害值
            int randomDamage = rand.Next(2, 5); // Next(2, 5)生成2,3,4中的一个数

            // 根据是否是己方打出，计算实际造成的伤害值（考虑法术伤害加成）
            int dmg = (ownplay) ? p.getSpellDamageDamage(randomDamage) : p.getEnemySpellDamageDamage(randomDamage);

            // 对目标造成指定伤害
            p.minionGetDamageOrHeal(target, dmg);

            // 计算实际造成的伤害值（目标当前生命值与受伤后生命值的差值）
            int actualDamage = Math.Min(target.maxHp, target.Hp + dmg) - target.Hp;

            // 根据实际造成的伤害值召唤相应数量的小鬼
            int posi = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
            for (int i = 0; i < actualDamage; i++)
            {
                p.callKid(kid, posi, ownplay);
                posi++;
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个随从作为伤害目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}