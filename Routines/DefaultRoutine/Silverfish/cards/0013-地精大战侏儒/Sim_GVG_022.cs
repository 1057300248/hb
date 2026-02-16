using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 修补匠的磨刀油卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为4点
    // 卡牌效果：使你的武器获得+3攻击力。<b>连击：</b>随机使一个友方随从获得+3攻击力。
    class Sim_GVG_022 : SimTemplate //* 修补匠的磨刀油 Tinker's Sharpsword Oil
    // Give your weapon +3 Attack. <b>Combo:</b> Give a random friendly minion +3 Attack.
    // 使你的武器获得+3攻击力。<b>连击：</b>随机使一个友方随从获得+3攻击力。 
    {
        // 重写卡牌打出时的效果方法，这是修补匠的磨刀油卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 处理己方打出的情况
            if (ownplay)
            {
                // 检查己方是否有武器且耐久度大于等于1
                if (p.ownWeapon.Durability >= 1)
                {
                    // 增加武器攻击力+3
                    p.ownWeapon.Angr += 3;

                    // 同时给己方英雄增加+3攻击力（因为武器攻击力会反映在英雄攻击力上）
                    p.minionGetBuffed(p.ownHero, 3, 0);
                }

                // 检查是否触发连击效果（本回合已打出至少一张卡牌）且有友方随从
                if (p.cardsPlayedThisTurn >= 1 && p.ownMinions.Count >= 1)
                {
                    // 创建随机数生成器
                    Random rand = new Random();

                    // 随机选择一个友方随从
                    Minion randomMinion = p.ownMinions[rand.Next(p.ownMinions.Count)];

                    // 给选中的随从增加+3攻击力
                    p.minionGetBuffed(randomMinion, 3, 0);
                }
            }
            else // 处理敌方打出的情况
            {
                // 检查敌方是否有武器且耐久度大于等于1
                if (p.enemyWeapon.Durability >= 1)
                {
                    // 增加敌方武器攻击力+3
                    p.enemyWeapon.Angr += 3;

                    // 同时给敌方英雄增加+3攻击力
                    p.minionGetBuffed(p.enemyHero, 3, 0);
                }

                // 检查是否触发连击效果且有敌方随从
                if (p.cardsPlayedThisTurn >= 1 && p.enemyMinions.Count >= 1)
                {
                    // 创建随机数生成器
                    Random rand = new Random();

                    // 随机选择一个敌方随从
                    Minion randomMinion = p.enemyMinions[rand.Next(p.enemyMinions.Count)];

                    // 给选中的随从增加+3攻击力
                    p.minionGetBuffed(randomMinion, 3, 0);
                }
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 修补匠的磨刀油不需要目标，可以直接打出
            // 注意：原代码中的REQ_MINION_TARGET是错误的，因为这张卡牌不需要选择目标
            return new PlayReq[] { };
        }
    }
}