using System;
using System.Windows.Forms;

namespace GameWhisAI
{

    public partial class Form1 : Form
    {
        int iUserChoise = 0;          // карта, выбранная пользователем
        int iAIChoise = 0;            // карта, выбранная ИИ
        int iStepNumber = 0;          // номер шага
        int[] aUserCards = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };  // карты пользователя
        int[] aAICards = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };    // карты ИИ
        int iDamageToUser = 0;        // урон пользователю
        int iDamageToAI = 0;          // урон полученный ИИ
        int iTotalDamageToUser = 0;   // весь урон пользователю
        int iTotalDamageToAI = 0;     // весь урон полученный ИИ
        bool bUserStarts = true;      // признак, что начинает игру пользователь
        int iUserScore = 0;           // счёт пользователя         
        int iAIScore = 0;             // счёт ИИ

        AI AI = new AI();
        CommonClass CommonClass = new CommonClass();
        public Form1()
        {
            InitializeComponent();
        }

        // метод проверки окончания раунда, вывода диалогового окна в конце раунда и перезапуска игры
        public void EndRound(ref int iStepNumber, ref bool bUserStarts, ref int iUserScore, ref int iAIScore, ref int iTotalDamageToUser, ref int iTotalDamageToAI)
        {
            if ((iStepNumber == 12 && bUserStarts == true) || (iStepNumber == 13 && bUserStarts == false))
            {
                string sWhoWon;
                if (iTotalDamageToUser < iTotalDamageToAI)
                {
                    sWhoWon = "Ура! Вы победили!";
                    iUserScore++;
                }
                else
                {
                    if (iTotalDamageToUser > iTotalDamageToAI)
                    {
                        sWhoWon = "Победил ИИ...";
                        iAIScore++;
                    }
                    else
                        sWhoWon = "Победила дружба!";
                }                
                string sMessage = "\tРаунд окончен,\n\n" + "\t" + sWhoWon + "\n\n" + "Урон Вам:\t" + iTotalDamageToUser + "\n" +
                   "Урон ИИ:\t\t" + iTotalDamageToAI + "\n\n" + "Вы выиграли игр:\t" + iUserScore + "\n" +
                   "ИИ выиграл игр:\t" + iAIScore + "\n\n" + "Хотите продолжить?";
                string sCaption = "Конец раунда";
                DialogResult result;
                result = MessageBox.Show(this, sMessage, sCaption, MessageBoxButtons.YesNo);
                if (result == DialogResult.No)                    
                        this.Close();                    
                if (result == DialogResult.Yes)
                    {
                        CommonClass.ResetVariables(ref iStepNumber, ref bUserStarts, ref iUserChoise, ref iAIChoise,
                              ref aUserCards, ref aAICards, ref iDamageToUser, ref iDamageToAI,
                              ref iTotalDamageToUser, ref iTotalDamageToAI);

                        // активация элементов управления 
                        label1.Text = "?";
                        label2.Text = "?";
                        label5.Text = "0";
                        label6.Text = "0";
                        label8.Text = default;
                        button1.Enabled = true; button2.Enabled = true; button3.Enabled = true; button4.Enabled = true;
                        button5.Enabled = true; button6.Enabled = true; button7.Enabled = true; button8.Enabled = true;
                        button9.Enabled = true; button10.Enabled = true; button11.Enabled = true; button12.Enabled = true;
                        button13.Enabled = true; button14.Enabled = true; button15.Enabled = true; button16.Enabled = true;
                        button17.Enabled = true; button18.Enabled = true; button19.Enabled = true; button20.Enabled = true;
                        button21.Enabled = true; button22.Enabled = true; button23.Enabled = true; button24.Enabled = true;

                        if (bUserStarts == false) 
                        {
                            label9.Text = "ИИ выбрал карту, защищайтесь!";
                        }
                        else                      
                        {
                            label9.Text = "   Выберите карту для атаки...";
                        }
                    }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {            
            if (iStepNumber % 2 == 0) // если TRUE - это атака пользователя
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 0;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button1);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else                        // если FALSE - это атака ИИ
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 0;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button1);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        // обработчики событий для кнопок пользователя 2 - 12. Идентичны обработчику для button1:
        private void button2_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 1;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button2);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 1;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button2);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 2;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button3);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 2;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button3);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 3;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button4);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 3;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button4);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 4;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button5);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 4;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button5);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 5;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button6);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 5;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button6);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 6;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button7);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 6;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button7);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 7;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button8);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 7;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button8);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 8;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button9);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 8;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button9);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 9;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button10);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 9;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button10);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 10;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button11);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser,
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 10;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button11);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if ((iStepNumber % 2) == 0)
            {
                BeforeChoiseUsersAttack(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 11;
                AfterChoiseUsersAttack(ref iDamageToAI, ref iTotalDamageToAI, ref aUserCards, button12);
                AfterChoiseUsersAttack_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, 
                                        ref iTotalDamageToAI, ref iAIChoise, ref aAICards, ref aUserCards);
            }
            else
            {
                BeforeChoiseUsersShild(ref aAICards, ref aUserCards, ref iAIChoise);
                iUserChoise = 11;
                AfterChoiseUsersShild(ref iDamageToUser, ref iTotalDamageToUser, ref aUserCards, button12);
                AfterChoiseUsersShild_2(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            }
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GameRullesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\t\t\tКарточная игра ДУЭЛЬ \n\n" +
              "\tВ эту игру играют два игрока, у каждого из которых на руках 12 карточек с числами от 0 до 11. " +
              "Первый игрок выбирает карту из имеющихся у него на руках и выкладывает на стол рубашкой вверх. \n" +
              "\tЧисло на выбранной карте будет являться АТАКОЙ игрока. После этого второй игрок выбирает " +
              "карту из оставшихся у него на руках и также выкладывает её рубашкой вверх. Это его ЗАЩИТА. " +
              "После этого игроки одновременно переворачивают карты, и защищающийся игрок получает столько " +
              "штрафных очков, насколько АТАКА первого игрока превышает ЗАЩИТУ второго. \n" +
              "\tВ следующем раунде игроки меняются местами. Игра заканчивается, когда у игроков не остаётся карт на руках. " +
              "Выигрывает игрок, получивший меньше всего штрафных очков.", "Правила игры");
        }

        public void BeforeChoiseUsersAttack(ref int[] aAICards, ref int[] aUserCards, ref int iAIChoise)
        {
            iAIChoise = AI.ChoosingAIProt_Attack(ref aAICards, ref aUserCards, iStepNumber, bUserStarts, label10);  // выбор карты ИИ для защиты, внесение изменения в массив карт ИИ
            // деактивация кнопки (в колоде ИИ)
            AI.ChoosingAI(iAIChoise, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24);
        }

        public void AfterChoiseUsersAttack(ref int iDamageToAI, ref int iTotalDamageToAI, ref int[] aUserCards, Button buttonX)
        {
            iDamageToAI = CommonClass.Damage(iUserChoise, iAIChoise);           // расчёт урона, полученного ИИ
            iTotalDamageToAI += iDamageToAI;                                    // расчёт полного урона, полученного ИИ с начала игры
            CommonClass.UserCardsChenge(iUserChoise, ref aUserCards, buttonX);  // выбор карты для атаки, внесение изменения в массив карт, деактивация кнопки
            // изиенение информации на форме
            CommonClass.TextOnForm(iUserChoise, iAIChoise, iTotalDamageToAI, iDamageToAI, iTotalDamageToUser, iDamageToUser, iStepNumber, label1, label2, label5, label6, label7, label8, label9);
        }
           
        public void AfterChoiseUsersAttack_2(ref int iStepNumber, ref bool bUserStarts, ref int iUserScore, ref int iAIScore, ref int iTotalDamageToUser, ref int iTotalDamageToAI,
                           ref int iAIChoise, ref int[] aAICards, ref int[] aUserCards)
            {
            iStepNumber += 1;
            // проверка окончания раунда
            EndRound(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
            // выбор карты ИИ для атаки, внесение изменения в массив карт ИИ
            if (bUserStarts == true && iStepNumber != 0)    // проверка, при перезапуске игры на первую атаку ИИ - не выполнять
                    iAIChoise = AI.ChoosingAIProt_Attack(ref aAICards, ref aUserCards, iStepNumber, bUserStarts, label10);
        }           
        public void BeforeChoiseUsersShild(ref int[] aAICards, ref int[] aUserCards, ref int iAIChoise)
        {
            if (bUserStarts == false)   // выполнять, если первым начинал атаковать пользователь
                // выбор карты ИИ для атаки, внесение изменения в массив карт ИИ
                iAIChoise = AI.ChoosingAIProt_Attack(ref aAICards, ref aUserCards, iStepNumber, bUserStarts, label10); 
            // деактивация кнопки (в колоде ИИ)
            AI.ChoosingAI(iAIChoise, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24);
        }
        public void AfterChoiseUsersShild(ref int iDamageToUser, ref int iTotalDamageToUser, ref int[] aUserCards, Button buttonX)
        {
            CommonClass.UserCardsChenge(iUserChoise, ref aUserCards, buttonX);  // выбор карты для защиты, внесение изменения в массив карт, деактивация кнопки
            iDamageToUser = CommonClass.Damage(iAIChoise, iUserChoise);         // расчёт урона, полученного пользователем
            iTotalDamageToUser += iDamageToUser;                                //расчёт полного урона, полученного пользователем с начала игры
            // изиенение информации на форме
            CommonClass.TextOnForm(iUserChoise, iAIChoise, iTotalDamageToAI, iDamageToAI, iTotalDamageToUser, iDamageToUser, iStepNumber, label1, label2, label5, label6, label7, label8, label9);
        }
        public void AfterChoiseUsersShild_2(ref int iStepNumber, ref bool bUserStarts, ref int iUserScore, ref int iAIScore, ref int iTotalDamageToUser, ref int iTotalDamageToAI)
        {
            iStepNumber += 1;
            // проверка окончания раунда
            EndRound(ref iStepNumber, ref bUserStarts, ref iUserScore, ref iAIScore, ref iTotalDamageToUser, ref iTotalDamageToAI);
        }
    }
}


