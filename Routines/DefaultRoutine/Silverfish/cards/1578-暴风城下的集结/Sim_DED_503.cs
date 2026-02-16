using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DED_503 : SimTemplate //* 暗影之刃飞刀手 Shadowblade Slinger
    {
        //战吼：如果你在本回合中受到过伤害，则对一个敌方随从造成等量的伤害。
        //Battlecry: If you've taken damage this turn, deal that much to an enemy minion.
        
            public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 基础伤害为2点
            int dmg = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);

            // 如果英雄在本回合受到过伤害，额外增加1点伤害
            if ((ownplay && p.ownHero.anzGotDmg > 0) || (!ownplay && p.enemyHero.anzGotDmg > 0))
            {
                dmg += (ownplay) ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
            }
        }
        
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
        
    }
}
