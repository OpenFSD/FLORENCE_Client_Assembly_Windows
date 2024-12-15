using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Exe.Wrt
{
    public class WriteEnable_Control
    {
        static private int coreId_For_WritePraise_Index;
        static private int[] count_CoreId_WriteActive;
        static private int[] count_CoreId_WriteIdle;
        static private int[] count_CoreId_WriteWait;
        static private bool[][] flag_WriteState;
        static private int new_coreId_For_WritePraise_Index;
        static private bool praisingWrite;
        static private int[] que_CoreToWrite;

        public WriteEnable_Control(
            FLORENCE.Frame.Cli.Global global,
            int numberOfCores
        )
        {
            coreId_For_WritePraise_Index = new int();
            coreId_For_WritePraise_Index = 0;

            count_CoreId_WriteActive = new int[4];//NUMBER OF CORES
            count_CoreId_WriteIdle = new int[4];//NUMBER OF CORES
            count_CoreId_WriteWait = new int[4];//NUMBER OF CORES
            for (int i = 0; i < numberOfCores; i++)
            {
                count_CoreId_WriteActive[i] = 0;
                count_CoreId_WriteIdle[i] = 0;
                count_CoreId_WriteWait[i] = 0;
            }
            
            flag_WriteState = new bool[4][];//NUMBER OF CORES
            for (int i = 0; i < numberOfCores; i++)
            {
                flag_WriteState[i] = new bool[2];
                for (int j = 0; j < 2; j++)
                {
                    flag_WriteState[i][j] = false;
                }
            }
            flag_WriteState[0][0] = global.GetConst_Write_IDLE(0);
            flag_WriteState[0][1] = global.GetConst_Write_IDLE(1);
            flag_WriteState[1][0] = global.GetConst_Write_IDLE(0);
            flag_WriteState[1][1] = global.GetConst_Write_IDLE(1);
            flag_WriteState[2][0] = global.GetConst_Write_IDLE(0);
            flag_WriteState[2][1] = global.GetConst_Write_IDLE(1);
            flag_WriteState[3][0] = global.GetConst_Write_IDLE(0);
            flag_WriteState[3][1] = global.GetConst_Write_IDLE(1);

            new_coreId_For_WritePraise_Index = new int();
            new_coreId_For_WritePraise_Index = 0;

            praisingWrite = new bool();
            praisingWrite = false;

            que_CoreToWrite = new int[4];//NUMBER OF CORES
            for (int index = 0; index < 4/* NUMBER OF CORES */; index++)
            {
                que_CoreToWrite[index] = index;
            }
        }

        public void WriteEnable_Activate(
            int coreId,
            FLORENCE.Frame.Cli.Global global,
            int numberOfCores
        )
        {
            for (int index = 0; index < 2; index++)
            {
                SetFlag_writeState(coreId, index, global.GetConst_Write_WRITE(index));
            }
        }
        public void WriteEnable_SortQue(
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            for (int index_A = 0; index_A < numberOfCores - 1; index_A++)
            {
                for (int index_B = index_A + 1; index_B < numberOfCores; index_B++)
                {
                    if (GetFlag_writeState(Get_que_CoreToWrite(index_A), 0) == global.GetConst_Write_WRITE(0)
                        && GetFlag_writeState(Get_que_CoreToWrite(index_A), 1) == global.GetConst_Write_WRITE(1))
                    {
                        if ((GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_WAIT(0)
                            && GetFlag_writeState(Get_que_CoreToWrite(index_B), 1) == global.GetConst_Write_WAIT(1))
                            || (GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_IDLE(0)
                                && GetFlag_writeState(Get_que_CoreToWrite(index_B), 1) == global.GetConst_Write_IDLE(1)))
                        {
                            WriteEnable_ShiftQueValues(index_A, index_B);
                        }
                        else if (GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_WRITE(0)
                            && GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_WRITE(1))
                        {
                            if (Get_count_WriteActive(index_A) > Get_count_WriteActive(index_B))
                            {
                                WriteEnable_ShiftQueValues(index_A, index_B);
                            }
                        }
                    }
                    else if (GetFlag_writeState(Get_que_CoreToWrite(index_A), 0) == global.GetConst_Write_IDLE(0)
                        && GetFlag_writeState(Get_que_CoreToWrite(index_A), 1) == global.GetConst_Write_IDLE(1))
                    {
                        if (GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_WAIT(0)
                            && GetFlag_writeState(Get_que_CoreToWrite(index_B), 1) == global.GetConst_Write_WAIT(1))
                        {
                            WriteEnable_ShiftQueValues(index_A, index_B);
                        }
                        else if (GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_IDLE(0)
                            && GetFlag_writeState(Get_que_CoreToWrite(index_B), 1) == global.GetConst_Write_IDLE(1))
                        {
                            if (Get_count_WriteIdle(index_A) < Get_count_WriteIdle(index_B))
                            {
                                WriteEnable_ShiftQueValues(index_A, index_B);
                            }
                        }
                    }
                    else if (GetFlag_writeState(Get_que_CoreToWrite(index_A), 0) == global.GetConst_Write_WAIT(0)
                        && GetFlag_writeState(Get_que_CoreToWrite(index_A), 1) == global.GetConst_Write_WAIT(1))
                    {
                        if (GetFlag_writeState(Get_que_CoreToWrite(index_B), 0) == global.GetConst_Write_WAIT(0)
                            && GetFlag_writeState(Get_que_CoreToWrite(index_B), 1) == global.GetConst_Write_WAIT(1))
                        {
                            if (Get_count_WriteWait(index_A) > Get_count_WriteWait(index_B))
                            {
                                WriteEnable_ShiftQueValues(index_A, index_B);
                            }
                        }
                    }
                }
            }
        }
        public void WriteEnable_Request(
            int coreId,
            int numberOfCores,
            FLORENCE.Frame.Cli.Global global
        )
        {
            while (GetFlag_readWrite_Open() == true)
            {
                DynamicStagger(coreId);
            }
            SetFlag_readWrite_Open(true);
            Set_coreIdForWritePraiseIndex(Get_new_coreIdForWritePraiseIndex());
            if (Get_coreIdForWritePraiseIndex() == coreId)
            {
                for (int index = 0; index < 2; index++)
                {
                    SetFlag_writeState(coreId, index, global.GetConst_Write_WAIT(index));
                }
                // Function exit.
            }
            else
            {
                Set_new_coreIdForWritePraiseIndex(Get_coreIdForWritePraiseIndex() + 1);
                if (Get_new_coreIdForWritePraiseIndex() == numberOfCores)
                {
                    Set_new_coreIdForWritePraiseIndex(0);
                }
                SetFlag_readWrite_Open(false);
                WriteEnable_Request(
                    coreId,
                    numberOfCores,
                    global

                );
            }
        }
        public void WriteQue_Update(
            int numberOfCores
        )
        {
            for (int index = 0; index < numberOfCores; index++)
            {
                if (GetFlag_writeState(index, 0) == false)
                {
                    if (GetFlag_writeState(index, 1) == false)
                    {
                        Set_count_WriteActive(index, 0);
                        Set_count_WriteIdle(index, Get_count_WriteIdle(index) + 1);
                        Set_count_WriteWait(index, 0);
                    }
                    else if (GetFlag_writeState(index, 1) == true)
                    {
                        Set_count_WriteActive(index, 0);
                        Set_count_WriteIdle(index, 0);
                        Set_count_WriteWait(index, Get_count_WriteWait(index) + 1);
                    }
                }
                else if (GetFlag_writeState(index, 0) == true)
                {
                    if (GetFlag_writeState(index, 1) == false)
                    {
                        Set_count_WriteActive(index, Get_count_WriteActive(index) + 1);
                        Set_count_WriteIdle(index, 0);
                        Set_count_WriteWait(index, 0);
                    }
                }
            }
        }

        public int Get_coreIdForWritePraiseIndex()
        {
            return coreId_For_WritePraise_Index;
        }
        private int Get_count_WriteActive(int coreId)
        {
            return count_CoreId_WriteActive[coreId];
        }
        private int Get_count_WriteIdle(int coreId)
        {
            return count_CoreId_WriteIdle[coreId];
        }
        private int Get_count_WriteWait(int coreId)
        {
            return count_CoreId_WriteWait[coreId];
        }
        private int GetFlag_CoreId_Of_WriteEnable()
        {
            return que_CoreToWrite[0];
        }
        public int Get_new_coreIdForWritePraiseIndex()
        {
            return new_coreId_For_WritePraise_Index;
        }
        private int Get_que_CoreToWrite(int coreId)
        {
            return que_CoreToWrite[coreId];
        }

        private void Set_count_WriteActive(int coreId, int value)
        {
            count_CoreId_WriteActive[coreId] = value;
        }
        private void Set_count_WriteIdle(int coreId, int value)
        {
            count_CoreId_WriteIdle[coreId] = value;
        }
        private void Set_count_WriteWait(int coreId, int value)
        {
            count_CoreId_WriteWait[coreId] = value;
        }
        public void SetFlag_readWrite_Open(bool value)
        {
            praisingWrite = value;
        }
        public void SetFlag_writeState(int coreId, int index, bool value)
        {
            flag_WriteState[coreId][index] = value;
        }
        public void Set_new_coreIdForWritePraiseIndex(int value)
        {
            new_coreId_For_WritePraise_Index = value;
        }
        private void Set_que_CoreToWrite(int index, int value)
        {
            que_CoreToWrite[index] = value;
        }

        private void DynamicStagger(
            int coreId
        )
        {
            if (Get_coreIdForWritePraiseIndex() == coreId)
            {
                //exit early
            }
            else
            {
                int ptr_count = 0;
                while (ptr_count < 20)//TODO record till flag change
                {
                    ptr_count = ptr_count + 1;
                }
            }
        }

        private void WriteEnable_ShiftQueValues(
            int coreId_A,
            int coreId_B
        )
        {
            int temp_A;
            temp_A = Get_count_WriteActive(coreId_A);
            Set_count_WriteActive(coreId_A, Get_count_WriteActive(coreId_B));
            Set_count_WriteActive(coreId_B, temp_A);

            temp_A = Get_count_WriteIdle(coreId_A);
            Set_count_WriteIdle(coreId_A, Get_count_WriteIdle(coreId_B));
            Set_count_WriteIdle(coreId_B, temp_A);

            temp_A = Get_count_WriteWait(coreId_A);
            Set_count_WriteWait(coreId_A, Get_count_WriteWait(coreId_B));
            Set_count_WriteWait(coreId_B, temp_A);

            int temp_B;
            temp_B = Get_que_CoreToWrite(coreId_A);
            Set_que_CoreToWrite(coreId_A, Get_que_CoreToWrite(coreId_B));
            Set_que_CoreToWrite(coreId_B, temp_B);
        }

        private bool GetFlag_readWrite_Open()
        {
            return praisingWrite;
        }

        private bool GetFlag_writeState(int coreId, int index)
        {
            return flag_WriteState[coreId][index];
        }

        private void Set_coreIdForWritePraiseIndex(int value)
        {
            coreId_For_WritePraise_Index = value;
        }

        internal void SetConditionCodeOfThisThreadedCore(object coreId)
        {
            throw new NotImplementedException();
        }

        internal bool GetFlag_SystemInitialised(object ptr_MyNumImplementedCores)
        {
            throw new NotImplementedException();
        }
    }
}
