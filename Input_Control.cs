using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Dat.In
{
    public class Input_Control
    {
        static private bool[] isSelected_PraiseEventId = new bool[0];
        static int numberOfPraises;

        public Input_Control()
        {
            numberOfPraises = 2;
            isSelected_PraiseEventId = new bool[numberOfPraises];
        }

        public void CheckBufferAnomalyInFlagArray()
        {
            for (int praiseEventId = 0; praiseEventId < numberOfPraises; praiseEventId++)
            {
                switch (praiseEventId)
                {
                    case 0:
                        if ((Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos().X == Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos().X)
                            || (Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos().Y == Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetMousePos().Y))
                        {
                            isSelected_PraiseEventId[praiseEventId] = true;
                        }
                        break;

                    case 1:
                        if (Framework.GetClient().GetData().GetInputBuffer(Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetPlayerPosition() != Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPlayer().GetPlayerPosition())
                        {
                            isSelected_PraiseEventId[praiseEventId] = true;
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
            Framework.GetClient().GetData().Flip_InBufferToWrite();
            for (int praiseEventId = 0; praiseEventId < numberOfPraises; praiseEventId++)
            {
                if (isSelected_PraiseEventId[praiseEventId] == true)
                {
                    Framework.GetClient().GetData().Set_New_InputBuffer(new FLORENCE.Frame.Cli.Dat.Input());
                    SelectSetInputSubset(praiseEventId);
                    LoadInputSubset(praiseEventId);
                    Framework.GetClient().GetData().GetData_Control().PushToStackOfInputActions(
                        Framework.GetClient().GetData().Get_StackOfInputActions(),
                        Framework.GetClient().GetData().GetNewInputBuffer()
                    );
                    isSelected_PraiseEventId[praiseEventId] = false;
                }
            }
        }
        private void LoadInputSubset(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
//===
//===
                case 0:
                    Framework.GetClient().GetData().GetNewInputBuffer().GetPraise0_Input().Set_Mouse_X(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise0_Input().Get_Mouse_X()
                    );
                    Framework.GetClient().GetData().GetNewInputBuffer().GetPraise0_Input().Set_Mouse_Y(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise0_Input().Get_Mouse_Y()
                    );
                    break;

                case 1:
                    Framework.GetClient().GetData().GetNewInputBuffer().GetPraise1_Input().Set_Position_X(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise1_Input().Get_Position_X()
                    );
                    Framework.GetClient().GetData().GetNewInputBuffer().GetPraise1_Input().Set_Position_Y(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise1_Input().Get_Position_Y()
                    );
                    Framework.GetClient().GetData().GetNewInputBuffer().GetPraise1_Input().Set_Position_Z(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise1_Input().Get_Position_Z()
                    );
                    break;
//===
//===
            }
        }
        private void SelectSetInputSubset(
            int praiseEventId
        )
        {
            switch (praiseEventId)
            {
//===
//===
                case 0:
                    Framework.GetClient().GetData().GetNewInputBuffer().Set_InputBuffer_SubSet(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise0_Input()
                    );
        			break;

		        case 1:
                    Framework.GetClient().GetData().GetNewInputBuffer().Set_InputBuffer_SubSet(
                        Framework.GetClient().GetData().GetInputBuffer(!Framework.GetClient().GetData().GetInBufferToWrite()).GetPraise1_Input()
                    );
                    break;
//===
//===
            }
		}
    }
}
