using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat.Out
{
    public class Output_Control
    {
        static private bool[] isSelected_PraiseEventId = new bool[0];
        static int numberOfPraises;

        public Output_Control() 
        {
            numberOfPraises = 2;
            isSelected_PraiseEventId = new bool[numberOfPraises];
        }

        public void CheckBufferAnomalyInFlagArray()
        {
            for (int praiseEventId_A = 0; praiseEventId_A < numberOfPraises; praiseEventId_A++)
            {
                switch (praiseEventId_A)
                {
                    case 0:
                        if (Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetMousePos() != Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetMousePos())
                        {
                            Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Set_MousePos(Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetMousePos());
                            isSelected_PraiseEventId[praiseEventId_A] = true;
                        }
                        break;

                    case 1:
                        if (Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetPlayerPosition() != Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetPlayerPosition())
                        {
                            Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().Set_PlayerPosition(Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetStateOfInBufferWrite()).GetPlayer().GetPlayerPosition());
                            isSelected_PraiseEventId[praiseEventId_A] = true;
                        }
                        break;

                    case 2:
                        break;

                    default:
                        break;
                }
            }
        }
        public void GenerateStackOfInputActions()
        { 
            for (int praiseEventId_B = 0; praiseEventId_B < numberOfPraises; praiseEventId_B++)
            {
                if (isSelected_PraiseEventId[praiseEventId_B] == true)
                {
                    SelectSetOutputSubsetForGivenPraiseEventId(praiseEventId_B);
                    
                    Networking.CreateAndSendNewMessage(praiseEventId_B);
                    isSelected_PraiseEventId[praiseEventId_B] = false;
                }
            }
        }
        void SelectSetOutputSubsetForGivenPraiseEventId(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
//===
//===
                case 0:
                    Framework.GetClient().GetData().GetThirdOutputBuffer().SetInputBufferSubSet(Framework.GetClient().GetData().GetUserIO().GetPraise1_Input());
                    break;

                case 1:
                    Framework.GetClient().GetData().GetThirdOutputBuffer().SetInputBufferSubSet(Framework.GetClient().GetData().GetUserIO().GetPraise0_Input());
                    break;
//===
//===
            }
        }
    }
}
