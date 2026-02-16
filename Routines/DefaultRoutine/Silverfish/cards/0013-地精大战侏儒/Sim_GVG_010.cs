using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 维伦的恩泽卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为3点
    // 卡牌效果：使一个随从获得+2/+4和<b>法术伤害+1</b>。
    class Sim_GVG_010 : SimTemplate //* 维伦的恩泽 Velen's Chosen
    // Give a minion +2/+4 and <b>Spell Damage +1</b>.
    // 使一个随从获得+2/+4和<b>法术伤害+1</b>。 
    {
        // 重写卡牌打出时的效果方法，这是维伦的恩泽卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 对目标随从应用+2攻击力和+4生命值的增益效果
            // 调用游戏场地的minionGetBuffed方法来修改随从的属性
            p.minionGetBuffed(target, 2, 4);

            // 增加目标随从自身的法术伤害加成（spellpower）
            // 这会影响该随从施放的法术伤害（如果有的话）
            target.spellpower++;

            // 根据目标随从的归属，更新对应方的全局法术伤害加成
            if (target.own)
            {
                // 如果目标随从属于己方，则增加己方的全局法术伤害加成
                p.spellpower++;
            }
            else
            {
                // 如果目标随从属于敌方，则增加敌方的全局法术伤害加成
                p.enemyspellpower++;
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}