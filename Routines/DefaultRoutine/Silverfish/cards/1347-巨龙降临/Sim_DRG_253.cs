namespace HREngine.Bots
{
    class Sim_DRG_253 : SimTemplate //* 矮人神射手 Dwarven Sharpshooter
    {
        //Your Hero Power can target minions.
        //你的英雄技能能够以随从为目标。
        public override void onAuraStarts(Playfield p, Minion m)
        {
            if (m.own) p.我们有热砂港狙击手 = true;
            else p.敌方有热砂港狙击手 = true;
        }

        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own)
            {
                bool hasss = false;
                foreach (Minion mnn in p.ownMinions)
                {
                    if (m.name == CardDB.cardNameEN.steamwheedlesniper && !mnn.silenced) hasss = true;
                }
                p.我们有热砂港狙击手 = hasss;
            }
            else
            {
                bool hasss = false;
                foreach (Minion mnn in p.enemyMinions)
                {
                    if (m.name == CardDB.cardNameEN.steamwheedlesniper && !mnn.silenced) hasss = true;
                }
                p.敌方有热砂港狙击手 = hasss;
            }
        }

    }
}