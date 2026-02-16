namespace HREngine.Bots
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 模拟下一个回合的AI决策器，用于计算最优行动方案
    /// </summary>
    public class MiniSimulatorNextTurn
    {
        //#####################################################################################################################
        //public int maxdeep = 6;
        //public int maxwide = 10;
        //public int totalboards = 50;

        /// <summary>
        /// 当前线程编号
        /// </summary>
        public int thread = 0;

        /// <summary>
        /// 是否使用惩罚管理器来评估局面
        /// </summary>
        private bool usePenalityManager = true;
        /// <summary>
        /// 是否使用目标削减优化
        /// </summary>
        private bool useCutingTargets = true;
        /// <summary>
        /// 是否避免重新计算
        /// </summary>
        private bool dontRecalc = true;
        /// <summary>
        /// 是否使用致命伤害检查
        /// </summary>
        private bool useLethalCheck = false;
        /// <summary>
        /// 是否使用局面比较功能
        /// </summary>
        private bool useComparison = true;

        /// <summary>
        /// 存储所有可能的局面状态列表
        /// </summary>
        List<Playfield> posmoves = new List<Playfield>(7000);

        /// <summary>
        /// 最优行动方案
        /// </summary>
        public Action bestmove = null;
        /// <summary>
        /// 最优行动的价值评分
        /// </summary>
        public float bestmoveValue = 0;
        /// <summary>
        /// 最优局面状态
        /// </summary>
        public Playfield bestboard = new Playfield();

        /// <summary>
        /// AI行为基类实例
        /// </summary>
        public Behavior botBase = null;
        /// <summary>
        /// 已计算的局面数量
        /// </summary>
        private int calculated = 0;

        /// <summary>
        /// 是否模拟第二个回合
        /// </summary>
        private bool simulateSecondTurn = false;

        /// <summary>
        /// 移动生成器单例实例
        /// </summary>
        Movegenerator movegen = Movegenerator.Instance;


        /// <summary>
        /// 初始化MiniSimulatorNextTurn实例
        /// </summary>
        public MiniSimulatorNextTurn()
        {
        }




        /// <summary>
        /// 开始模拟敌方回合
        /// </summary>
        /// <param name="p">当前局面状态</param>
        /// <param name="simulateTwoTurns">是否模拟两个回合</param>
        /// <param name="print">是否打印调试信息</param>
        /// <param name="playaround">是否进行随机干扰模拟</param>
        /// <param name="playaroundprob">第一次随机干扰概率</param>
        /// <param name="playaroundprob2">第二次随机干扰概率</param>
        private void startEnemyTurnSim(Playfield p, bool simulateTwoTurns, bool print, bool playaround, int playaroundprob, int playaroundprob2)
        {
            if (p.guessingHeroHP >= 1)
            {

                Ai.Instance.enemySecondTurnSim[this.thread].simulateEnemysTurn(p, simulateTwoTurns, playaround, print, playaroundprob, playaroundprob2);
            }
            p.complete = true;
        }

        /// <summary>
        /// 执行所有可能的移动并找到最佳策略
        /// </summary>
        /// <param name="playf">初始局面状态</param>
        /// <param name="print">是否打印调试信息</param>
        /// <returns>最佳局面价值评分</returns>
        public float doallmoves(Playfield playf, bool print = false)
        {
            //todo only one time!
            bool isLethalCheck = playf.isLethalCheck;
            int totalboards = Settings.Instance.nextTurnTotalBoards;
            int maxwide = Settings.Instance.nextTurnMaxWide;
            int maxdeep = Settings.Instance.nextTurnDeep;
            bool playaround = Settings.Instance.playaround;
            int playaroundprob = Settings.Instance.playaroundprob;
            int playaroundprob2 = Settings.Instance.playaroundprob2;

            botBase = Ai.Instance.botBase;
            this.posmoves.Clear();
            this.posmoves.Add(new Playfield(playf));
            bool havedonesomething = true;
            List<Playfield> temp = new List<Playfield>();
            int deep = 0;
            this.calculated = 0;
            Playfield bestold = null;
            float bestoldval = -20000000;
            while (havedonesomething)
            {
                //GC.Collect();
                temp.Clear();
                temp.AddRange(this.posmoves);
                havedonesomething = false;
                foreach (Playfield p in temp)
                {
                    if (p.complete || p.ownHero.Hp <= 0)
                    {
                        continue;
                    }

                    List<Action> actions = movegen.getMoveList(p, usePenalityManager, useCutingTargets, true);
                    foreach (Action a in actions)
                    {
                        havedonesomething = true;
                        Playfield pf = new Playfield(p);
                        pf.doAction(a);
                        if (pf.ownHero.Hp > 0) this.posmoves.Add(pf);
                        if (totalboards > 0) this.calculated++;
                    }


                    p.endTurn();

                    if (!isLethalCheck) this.startEnemyTurnSim(p, this.simulateSecondTurn, false, playaround, playaroundprob, playaroundprob2);

                    //sort stupid stuff ouf

                    if (botBase.getPlayfieldValue(p) > bestoldval)
                    {
                        bestoldval = botBase.getPlayfieldValue(p);
                        bestold = p;
                    }
                    posmoves.Remove(p);

                    if (this.calculated > totalboards) break;
                }
                cuttingposibilities(maxwide);

                deep++;

                if (this.calculated > totalboards) break;
                if (deep >= maxdeep) break;
            }

            posmoves.Add(bestold);
            foreach (Playfield p in posmoves)
            {
                if (!p.complete)
                {

                    p.endTurn();
                    if (!isLethalCheck) this.startEnemyTurnSim(p, this.simulateSecondTurn, false, playaround, playaroundprob, playaroundprob2);
                }
            }
            // find best

            if (posmoves.Count >= 1)
            {
                posmoves.Sort((a, b) => botBase.getPlayfieldValue(b).CompareTo(botBase.getPlayfieldValue(a)));

                Playfield bestplay = posmoves[0];
                float bestval = botBase.getPlayfieldValue(bestplay);
                int pcount = posmoves.Count;
                for (int i = 1; i < pcount; i++)
                {
                    float val = botBase.getPlayfieldValue(posmoves[i]);
                    if (bestval > val) break;
                    if (bestplay.playactions.Count <= posmoves[i].playactions.Count) continue; //priority to the minimum acts
                    bestplay = posmoves[i];
                    bestval = val;
                }
                this.bestmove = bestplay.getNextAction();
                this.bestmoveValue = bestval;
                this.bestboard = new Playfield(bestplay);
                return bestval;
            }
            this.bestmove = null;
            this.bestmoveValue = -2000000;
            this.bestboard = playf;
            return -2000000;
        }

        /// <summary>
        /// 削减可能性，保留最有价值的局面
        /// </summary>
        /// <param name="maxwide">最大保留局面数量</param>
        public void cuttingposibilities(int maxwide)
        {
            // take the x best values
            List<Playfield> temp = new List<Playfield>();
            Dictionary<Int64, Playfield> tempDict = new Dictionary<Int64, Playfield>();
            try
            {
                posmoves.Sort((a, b) => -(botBase.getPlayfieldValue(a)).CompareTo(botBase.getPlayfieldValue(b)));//want to keep the best
            }
            catch (Exception e)
            {
                Helpfunctions.Instance.logg("异常:" + e.Message); //Todo: 待Fix 不应该有异常，猜测是因为相同场面因为牌序不同，得分不同，可以考虑先去重再排序
            }

            if (this.useComparison)
            {
                int i = 0;
                int max = Math.Min(posmoves.Count, maxwide);

                Playfield p = null;
                //foreach (Playfield p in posmoves)
                for (i = 0; i < max; i++)
                {
                    p = posmoves[i];
                    Int64 hash = p.GetPHash();
                    p.hashcode = hash;
                    if (!tempDict.ContainsKey(hash)) tempDict.Add(hash, p);

                }
                foreach (KeyValuePair<Int64, Playfield> d in tempDict)
                {
                    temp.Add(d.Value);
                }
            }
            else
            {
                temp.AddRange(posmoves);
            }
            posmoves.Clear();
            posmoves.AddRange(temp.GetRange(0, Math.Min(maxwide, temp.Count)));

        }

        /// <summary>
        /// 削减攻击目标，去除重复或不必要的攻击目标
        /// </summary>
        /// <param name="oldlist">原始目标列表</param>
        /// <param name="p">当前局面</param>
        /// <param name="own">是否为己方单位</param>
        /// <returns>优化后的目标列表</returns>
        public List<targett> cutAttackTargets(List<targett> oldlist, Playfield p, bool own)
        {
            List<targett> retvalues = new List<targett>();
            List<Minion> addedmins = new List<Minion>(8);

            bool priomins = false;
            List<targett> retvaluesPrio = new List<targett>();
            foreach (targett t in oldlist)
            {
                if ((own && t.target == 200) || (!own && t.target == 100))
                {
                    retvalues.Add(t);
                    continue;
                }
                if ((own && t.target >= 10 && t.target <= 19) || (!own && t.target >= 0 && t.target <= 9))
                {
                    Minion m = null;
                    if (own) m = p.enemyMinions[t.target - 10];
                    if (!own) m = p.ownMinions[t.target];
                    /*if (penman.priorityDatabase.ContainsKey(m.name))
                    {
                        //retvalues.Add(t);
                        retvaluesPrio.Add(t);
                        priomins = true;
                        //help.logg(m.name + " is added to targetlist");
                        continue;
                    }*/


                    bool goingtoadd = true;
                    List<Minion> temp = new List<Minion>(addedmins);
                    bool isSpecial = m.handcard.card.isSpecialMinion;
                    foreach (Minion mnn in temp)
                    {
                        // special minions are allowed to attack in silended and unsilenced state!
                        //help.logg(mnn.silenced + " " + m.silenced + " " + mnn.name + " " + m.name + " " + penman.specialMinions.ContainsKey(m.name));

                        bool otherisSpecial = mnn.handcard.card.isSpecialMinion;

                        if ((!isSpecial || (isSpecial && m.silenced)) && (!otherisSpecial || (otherisSpecial && mnn.silenced))) // both are not special, if they are the same, dont add
                        {
                            if (mnn.Angr == m.Angr && mnn.Hp == m.Hp && mnn.divineshild == m.divineshild && mnn.reborn == m.reborn && mnn.zerg == m.zerg && mnn.taunt == m.taunt && mnn.poisonous == m.poisonous && mnn.lifesteal == m.lifesteal) goingtoadd = false;
                            continue;
                        }

                        if (isSpecial == otherisSpecial && !m.silenced && !mnn.silenced) // same are special
                        {
                            if (m.name != mnn.name) // different name -> take it
                            {
                                continue;
                            }
                            // same name -> test whether they are equal
                            if (mnn.Angr == m.Angr && mnn.Hp == m.Hp && mnn.divineshild == m.divineshild && mnn.reborn == m.reborn && mnn.zerg == m.zerg && mnn.taunt == m.taunt && mnn.poisonous == m.poisonous && mnn.lifesteal == m.lifesteal) goingtoadd = false;
                            continue;
                        }

                    }

                    if (goingtoadd)
                    {
                        addedmins.Add(m);
                        retvalues.Add(t);
                        //help.logg(m.name + " " + m.id +" is added to targetlist");
                    }
                    else
                    {
                        //help.logg(m.name + " is not needed to attack");
                        continue;
                    }

                }
            }
            //help.logg("end targetcutting");
            if (priomins) return retvaluesPrio;

            return retvalues;
        }

        /// <summary>
        /// 打印所有可能的局面状态
        /// </summary>
        public void printPosmoves()
        {
            foreach (Playfield p in this.posmoves)
            {
                p.printBoard();
            }
        }

    }


}