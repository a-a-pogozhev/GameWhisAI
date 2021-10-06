using System.Windows.Forms;

namespace GameWhisAI
{
    public class CommonClass
    {
        // метод расчёта полученного урона
        public int Damage(int iAttack, int iShild)
        {
            int iDamage = 0;
            if (iShild < iAttack)
                iDamage = iAttack - iShild;
            return iDamage;
        }
        // метод изменения карты в массиве на признак "использованная карта", деактивация кнопки
        public void UserCardsChenge(int iUserChoise, ref int[] aUserCards, Button button)
        {
            aUserCards[iUserChoise] = 64;
            button.Enabled = false;
        }
        //  метод вывода на форму массивов (для отладки)
        public string ArrayOut(int[] aCards)
        {
            string sArrayText = "";
            for (int i = 0; i <= 11; i++)
            {
                sArrayText = sArrayText + " | " + aCards[i].ToString();
            }
            return sArrayText;
        }
        // метод вывода на форму игровой информации и комментариев
        public void TextOnForm(int iUserChoise, int iAIChoise, int iTotalDamageToAI, int iDamageToAI, int iTotalDamageToUser,
                    int iDamageToUser, int iStepNumber, Label label1, Label label2, Label label5, Label label6,
                    Label label7, Label label8, Label label9)
        {
            label1.Text = iUserChoise.ToString();
            label2.Text = iAIChoise.ToString();
            if ((iStepNumber % 2) == 0) // если TRUE - это атака пользователя
            {
                label6.Text = iTotalDamageToAI.ToString();
                label8.Text = iDamageToAI.ToString();
                label9.Text = "ИИ выбрал карту, защищайтесь!";
            
            }
            else                        // если FALSE - это атака ИИ
            {
                label5.Text = iTotalDamageToUser.ToString();
                label8.Text = iDamageToUser.ToString();
                label9.Text = "   Выберите карту для атаки...";            
            }
        }
        // метод сброса переменных перед следующим раундом
        public void ResetVariables(ref int iStepNumber, ref bool bUserStarts, ref int iUserChoise, ref int iAIChoise,
                ref int[] aUserCards, ref int[] aAICards, ref int iDamageToUser, ref int iDamageToAI,
                ref int iTotalDamageToUser, ref int iTotalDamageToAI)
        {
            iUserChoise = 0;
            iAIChoise = 0;           
            iDamageToUser = 0;
            iDamageToAI = 0;
            iTotalDamageToUser = 0;
            iTotalDamageToAI = 0;

            for (int j = 0; j <= 11; j++)            
                aUserCards[j] = j;            
            for (int j = 0; j <= 11; j++)
                aAICards[j] = j;

            if (bUserStarts == true)
            {
                iStepNumber = 1;
                bUserStarts = false;
            }
            else
            {
                iStepNumber = 0;
                bUserStarts = true;
            }
        }
    }
}
