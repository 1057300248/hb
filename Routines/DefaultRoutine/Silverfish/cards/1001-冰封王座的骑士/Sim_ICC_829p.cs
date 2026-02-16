using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_ICC_829p: SimTemplate //* 天启四骑士 The Four Horsemen
                                    //[x]<b>Hero Power</b>Summon a 2/2 Horseman.If you have all 4, destroythe enemy hero.
                                    //<b>英雄技能</b>召唤一个2/2的天启骑士。如果你控制所有四个天启骑士，消灭敌方英雄。 
    {

        // 添加静态 Random 实例解决随机问题
        private static System.Random random = new System.Random();

        CardDB.Card kid1 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ICC_829t2);
        CardDB.Card kid2 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ICC_829t3);
        CardDB.Card kid3 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ICC_829t4);
        CardDB.Card kid4 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ICC_829t5);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int pos = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;
            CardDB.cardIDEnum[] allKids = new CardDB.cardIDEnum[]
            {
                CardDB.cardIDEnum.ICC_829t2,
                CardDB.cardIDEnum.ICC_829t3,
                CardDB.cardIDEnum.ICC_829t4,
                CardDB.cardIDEnum.ICC_829t5
            };

            System.Collections.Generic.List<CardDB.cardIDEnum> existingIDs = new System.Collections.Generic.List<CardDB.cardIDEnum>();
            foreach (Minion m in (ownplay ? p.ownMinions : p.enemyMinions))
            {
                CardDB.cardIDEnum id = m.handcard.card.cardIDenum;
                if (System.Array.Exists(allKids, kid => kid == id) && !existingIDs.Contains(id))
                {
                    existingIDs.Add(id);
                }
            }

            if (existingIDs.Count == 0)
            {
                p.callKid(kid1, pos, ownplay, false);
            }
            else
            {
                System.Collections.Generic.List<CardDB.cardIDEnum> candidates = new System.Collections.Generic.List<CardDB.cardIDEnum>();
                foreach (CardDB.cardIDEnum kid in allKids)
                {
                    if (!existingIDs.Contains(kid))
                    {
                        candidates.Add(kid);
                    }
                }

                // 使用静态 random 实例替代 p.random
                int randIndex = random.Next(candidates.Count);
                CardDB.Card selectedKid = CardDB.Instance.getCardDataFromID(candidates[randIndex]);
                p.callKid(selectedKid, pos, ownplay, false);
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 2),
            };
        }
    }
}