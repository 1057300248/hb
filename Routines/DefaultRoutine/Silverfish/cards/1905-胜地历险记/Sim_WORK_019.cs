using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 猎人 费用：5 攻击力：3 生命值：7
	//Cash Cow
	//摇钱金牛
	//[x]<b>Taunt</b>Whenever this takesdamage, get a Coin.
	//<b>嘲讽</b>。每当本随从受到伤害，获取一张幸运币。
	class Sim_WORK_019 : SimTemplate
	{
        public override void onMinionGotDmgTrigger(Playfield p, Minion triggerEffectMinion, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 如果触发此效果的随从是摇钱金牛
            if (triggerEffectMinion.name == CardDB.cardNameEN.workhorse && triggerEffectMinion.Hp <= triggerEffectMinion.maxHp)
            {
                // 处理己方玩家
                if (triggerEffectMinion.own)
                {
                    if (p.owncards.Count < 10) // 检查己方手牌数量是否小于10
                    {
                        p.drawACard(CardDB.cardIDEnum.GAME_005, triggerEffectMinion.own, true); // 给己方玩家增加一张幸运币
                    }
                }
                else
                {
                    // 处理敌方玩家
                    if (p.enemyAnzCards < 10) // 检查敌方手牌数量是否小于10
                    {
                        p.drawACard(CardDB.cardIDEnum.GAME_005, triggerEffectMinion.own, true); // 给敌方玩家增加一张幸运币
                    }
                }
            }
        }
    }
}
