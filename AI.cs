using System;
using System.Windows.Forms;

namespace GameWhisAI
{
    public class AI
    {
        Random Rnd = new Random();
        int iRndCard, iUserMaxCard, iUserMinCard, iAIMaxCard, iAIMinCard;

        // метод выбора карты ИИ
        public int ChoosingAIProt_Attack(ref int[] aAICards, ref int[] aUserCards, int iStepNumber, bool bUserStarts, Label label10)
        {
            int i;
            // выбор карты ИИ на предпоследнем ходе для эффективной игры на последнем ходе
            if ((iStepNumber == 10 && bUserStarts == true) || (iStepNumber == 11 && bUserStarts == false))
            {
                iUserMinCard = SearchMinCard(ref aUserCards);
                iAIMinCard = SearchMinCard(ref aAICards);
                iUserMaxCard = SearchMaxCard(ref aUserCards);
                iAIMaxCard = SearchMaxCard(ref aAICards);

                // выбор карты ИИ на предпоследнем ходе для эффективной атаки на последнем ходе
                if (iStepNumber == 10 && bUserStarts == true)
                {
                    if (iAIMinCard >= iUserMinCard && iAIMinCard >= iUserMaxCard)
                    { iRndCard = iAIMinCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard <= iUserMinCard && iAIMaxCard <= iUserMaxCard)
                    { iRndCard = iAIMaxCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard >= iUserMinCard && iAIMaxCard <= iUserMaxCard)
                    { iRndCard = iAIMinCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard <= iUserMinCard && iAIMaxCard >= iUserMaxCard)
                    { iRndCard = iAIMinCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard <= iUserMinCard && iAIMaxCard <= iUserMinCard)
                    { iRndCard = iAIMaxCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard >= iUserMinCard && iAIMaxCard >= iUserMaxCard)
                    { iRndCard = iAIMinCard;    aAICards[iRndCard] = 64;    goto EndMethod; }
                }
                // выбор карты ИИ на предпоследнем ходе для эффективной защиты на последнем ходе
                else
                {
                    if (iAIMinCard >= iUserMinCard && iAIMinCard >= iUserMaxCard)
                    { iRndCard = iAIMaxCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard <= iUserMinCard && iAIMaxCard <= iUserMaxCard)
                    { iRndCard = iAIMinCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard >= iUserMinCard && iAIMaxCard <= iUserMaxCard)
                    { iRndCard = iAIMaxCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard <= iUserMinCard && iAIMaxCard >= iUserMaxCard)
                    { iRndCard = iAIMaxCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard <= iUserMinCard && iAIMaxCard <= iUserMinCard)
                    { iRndCard = iAIMinCard;    aAICards[iRndCard] = 64;    goto EndMethod; }

                    if (iAIMinCard >= iUserMinCard && iAIMaxCard >= iUserMaxCard)
                    { iRndCard = iAIMaxCard;    aAICards[iRndCard] = 64;    goto EndMethod; }
                }
                // выбор карты на всех ходах, кроме предпоследнего
            }
            else
            {
                i = 0;
                bool bRightChoice = false;
                // выбор карты ИИ для защиты
                if (iStepNumber % 2 == 0)
                {
                    do
                    {
                        i++;
                        iRndCard = Rnd.Next(4, 10);
                        if (aAICards[iRndCard] != 64)
                        {
                            aAICards[iRndCard] = 64;
                            bRightChoice = true;
                        }
                        // если в диапазоне 4 - 9 нет карт, тогда ищем во всём диапазоне
                        if (i > 36)
                        {                            
                            bRightChoice = FullRange(ref aAICards, ref iRndCard);
                        }
                    }
                    while (bRightChoice == false);
                }
                else    // выбор карты для атаки
                {                    
                    bRightChoice = FullRange(ref aAICards, ref iRndCard);
                }
            }

        EndMethod:
            return iRndCard;
        }
        // метод деактивации кнопок ИИ
        public void ChoosingAI(int iRndCard, Button button13, Button button14, Button button15, Button button16, Button button17,
          Button button18, Button button19, Button button20, Button button21, Button button22, Button button23, Button button24)
        {
            switch (iRndCard)
            {
                case 0:     button13.Enabled = false;   break;
                case 1:     button14.Enabled = false;   break;
                case 2:     button15.Enabled = false;   break;
                case 3:     button16.Enabled = false;   break;
                case 4:     button17.Enabled = false;   break;
                case 5:     button18.Enabled = false;   break;
                case 6:     button19.Enabled = false;   break;
                case 7:     button20.Enabled = false;   break;
                case 8:     button21.Enabled = false;   break;
                case 9:     button22.Enabled = false;   break;
                case 10:    button23.Enabled = false;   break;
                case 11:    button24.Enabled = false;   break;
            }
        }
        // метод поиска минимальной карты в колоде
        private int SearchMinCard(ref int[] aCards)
        {
            int j;
            int iMinCard = -1;
            for (j = 0; j <= 11; j++)
            {
                if (aCards[j] != 64)
                {
                    iMinCard = aCards[j];
                    break;
                }
            }
            return iMinCard;
        }
        // метод поиска максимальной карты в колоде
        private int SearchMaxCard(ref int[] aCards)
        {
            int j;
            int iMaxCard = -1;
            for (j = 11; j >= 0; j--)
            {
                if (aCards[j] != 64)
                {
                    iMaxCard = aCards[j];
                    break;
                }
            }
            return iMaxCard;
        }

        // метод выбора карты из всего диапазона
        private bool FullRange(ref int[] aAICards, ref int iRndCard)
        {
            bool bRightChoice = false;
            do
            {
                iRndCard = Rnd.Next(0, 12);
                if (aAICards[iRndCard] != 64)
                {
                    aAICards[iRndCard] = 64;
                    bRightChoice = true;
                }
            }
            while (bRightChoice == false);
            return bRightChoice;
        }
    }

}
