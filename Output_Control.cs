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
                        if (Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos() != Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos())
                        {
                            Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().Set_MousePos(Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos());
                            isSelected_PraiseEventId[praiseEventId_A] = true;
                        }
                        break;

                    case 1:
                        if (Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetPlayerPosition() != Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetPlayerPosition())
                        {
                            Framework.GetClient().GetData().GetOutputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().Set_PlayerPosition(Framework.GetClient().GetData().GetOutputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetPlayerPosition());
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
                    SelectSetInputSubsetForGivenPraiseEventId(praiseEventId_B);
                    
                    Networking.CreateAndSendNewMessage(praiseEventId_B);
                    isSelected_PraiseEventId[praiseEventId_B] = false;
                }
            }
        }
        void SelectSetInputSubsetForGivenPraiseEventId(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
                //===
                //===
                case 0:
                    Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).Set_InputBuffer_SubSet(
                        Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise0_Input()
                    );
                    break;

                case 1:

                    break;
                    //===
                    //===
            }
        }
    }
}
