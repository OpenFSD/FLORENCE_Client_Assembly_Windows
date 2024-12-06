using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLORENCE.Frame.Cli.Algo
{ 
    public class IO_ListenRespond
    {
        public IO_ListenRespond() 
        { 
        
        }
        public void InitialiseControl()
        {

        }

        public void Initialise(int numberOfCores)
        {

        }

        static public void Thread_io_ListenRespond()
        {
            while (true)
            {
                Framework.GetClient().GetExecute().GetExecute_Control().SetConditionCodeOfThisThreadedCore((Int16)1);
                while (Framework.GetClient().GetExecute().GetExecute_Control().GetFlag_SystemInitialised(Framework.GetClient().GetGlobal().Get_NumCores()) != false)
                {
                    // wait untill ALL threads initalised in preperation of system init.
                }
                switch (FLORENCE::framework::Get_Server()->Get_Algorithms()->Get_ListenRespond()->Get_Control_Of_ListenRespond()->GetFlag_IO_ThreadState())
                {
                    case true:
                    {
                        Framework.GetClient().GetExecute().GetWriteEnable().Write_Start(
                            Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                            0,
                            Framework.GetClient().GetGlobal().Get_NumCores(),
                            Framework.GetClient().GetGlobal()
                        );

                        //TODO> client praise networking accepted and captured
                        FLORENCE::framework::Get_Server()->Get_Data()->Get_PraiseBuffer()->SetPraiseEventId(0);//NETWORKING TODO
                        FLORENCE::framework::Get_Server()->Get_Data()->Get_PraiseBuffer()->Set_InputBuffer_SubSet(
                            FLORENCE::framework::Get_Server()->Get_Data()->Get_PraiseBuffer()->Get_InputBufferSubset()
                        );
                        FLORENCE::framework::Get_Server()->Get_Data()->Get_PraiseBuffer()->Get_InputBufferSubset()->Set_A(new bool(false));
                        FLORENCE::framework::Get_Server()->Get_Data()->Get_PraiseBuffer()->Get_InputBufferSubset()->Set_B(new bool(false));
                        //END TODO> client praise networking accepted and captured

                        FLORENCE::framework::Get_Server()->Get_Data()->Get_Control_Of_Data()->PushToStackOfInputPraises(
                            FLORENCE::framework::Get_Server()->Get_Data()->Get_StackOfInputPraise(),
                            FLORENCE::framework::Get_Server()->Get_Data()->Get_PraiseBuffer()
                        );

                        FLORENCE::framework::Get_Server()->Get_Data()->Get_Control_Of_Data()->SetFlag_InputStackLoaded(true);

                        while (FLORENCE::framework::Get_Server()->Get_Execute()->Get_LaunchConcurrency()->Get_Control_Of_LaunchConcurrency()->GetFlag_ConcurrentCoreState(
                            FLORENCE::framework::Get_Server()->Get_Execute()->Get_LaunchConcurrency()->Get_Control_Of_LaunchConcurrency()->Get_coreId_To_Launch()) == FLORENCE::framework::Get_Server()->Get_Global()->GetConst_Core_ACTIVE()
                            )
                        {/* wait untill a core is free */
                        }

                        FLORENCE::framework::Get_Server()->Get_Execute()->Get_LaunchConcurrency()->Concurrent_Thread_Start(
                            FLORENCE::framework::Get_Server()->Get_Execute()->Get_LaunchConcurrency()->Get_Control_Of_LaunchConcurrency(),
                            FLORENCE::framework::Get_Server()->Get_Execute()->Get_LaunchConcurrency()->Get_Control_Of_LaunchConcurrency()->Get_coreId_To_Launch(),
                            FLORENCE::framework::Get_Server()->Get_Global(),
                            ptr_MyNumImplementedCores
                        );//Dynamic Launch

                        Framework.GetClient().GetExecute().GetWriteEnable().Write_End(
                            Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                            0,
                            Framework.GetClient().GetGlobal().Get_NumCores(),
                            Framework.GetClient().GetGlobal()
                        );

                        FLORENCE::framework::Get_Server()->Get_Algorithms()->Get_ListenRespond()->Get_Control_Of_ListenRespond()->SetFlag_IO_ThreadState(false);//DISTRIBUTE=FALSE
                        break;
                    }
                    case false:
                    {
                            if (FLORENCE::framework::Get_Server()->Get_Data()->Get_Control_Of_Data()->GetFlag_OutputStackLoaded() == true)
                            {
                                Framework.GetClient().GetExecute().GetWriteEnable().Write_Start(
                                    Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                                    0,
                                    Framework.GetClient().GetGlobal().Get_NumCores(),
                                    Framework.GetClient().GetGlobal()
                                );
                                FLORENCE::framework::Get_Server()->Get_Data()->Get_Control_Of_Data()->PopFromStackOfOutput(
                                    FLORENCE::framework::Get_Server()->Get_Data()->Get_DistributeBuffer(),
                                    FLORENCE::framework::Get_Server()->Get_Data()->Get_StackOfDistributeBuffer()
                                );
                                if (sizeof(FLORENCE::framework::Get_Server()->Get_Data()->Get_StackOfDistributeBuffer()) < 1)
                                {
                                    FLORENCE::framework::Get_Server()->Get_Data()->Get_Control_Of_Data()->SetFlag_OutputStackLoaded(false);
                                }
                                else
                                {
                                    FLORENCE::framework::Get_Server()->Get_Data()->Get_Control_Of_Data()->SetFlag_OutputStackLoaded(true);
                                }
                                //TODO> FLORENCE::framework::Get_Server() distribute networking
                                /*
                                *  send registers in distribute buffer
                                *  set ACK distribute sent to equal TRUE
                                */
                                Framework.GetClient().GetExecute().GetWriteEnable().Write_End(
                                    Framework.GetClient().GetExecute().GetWriteEnable().GetWriteEnable_Contorl(),
                                    0,
                                    Framework.GetClient().GetGlobal().Get_NumCores(),
                                    Framework.GetClient().GetGlobal()
                                );
                            }
                            FLORENCE::framework::Get_Server()->Get_Algorithms()->Get_ListenRespond()->Get_Control_Of_ListenRespond()->SetFlag_IO_ThreadState(true);//LISTEN=TRUE
                            break;
                        }
                }
            }
        }
    }
}
