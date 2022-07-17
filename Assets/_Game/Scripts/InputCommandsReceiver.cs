using UnityEngine;

public class InputCommandsReceiver : MonoBehaviour
{
    private void Update()
    {
        switch (GameDelegatesContainer.FuncGameState())
        { 
            case GameStateType.CubeTurns:
                if (Input.GetKeyDown(KeyCode.G))
                {
                    InputDelegatesContainer.CubeTurnsCommand();
                }
                return;
            case GameStateType.InsideCube:
                if (Input.GetKeyDown(KeyCode.G))
                {
                    InputDelegatesContainer.CubeTurnsCommand();
                    return;
                }


                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols() == 1)
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols() == 2)
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols() == 3)
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(3);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    if (GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols() == 4)
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(4);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    if (GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols() == 5)
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(5);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    if (GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols() == 6)
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(6);
                }
                return;
        }
    }
}