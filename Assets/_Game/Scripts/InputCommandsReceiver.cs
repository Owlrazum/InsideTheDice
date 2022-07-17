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

                if (GameDelegatesContainer.FuncPlayerState() != PlayerStateType.Idle)
                {
                    return;
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (GameDelegatesContainer.FuncCurrentSideDotCount() > 0)
                    {
                        return;
                    }
                    if (GameDelegatesContainer.FuncIsDotCountAlreadyPlaced(1))
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (GameDelegatesContainer.FuncCurrentSideDotCount() > 0)
                    {
                        return;
                    }
                    if (GameDelegatesContainer.FuncIsDotCountAlreadyPlaced(2))
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (GameDelegatesContainer.FuncCurrentSideDotCount() > 0)
                    {
                        return;
                    }
                    if (GameDelegatesContainer.FuncIsDotCountAlreadyPlaced(3))
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(3);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    if (GameDelegatesContainer.FuncCurrentSideDotCount() > 0)
                    {
                        return;
                    }
                    if (GameDelegatesContainer.FuncIsDotCountAlreadyPlaced(4))
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(4);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    if (GameDelegatesContainer.FuncCurrentSideDotCount() > 0)
                    {
                        return;
                    }
                    if (GameDelegatesContainer.FuncIsDotCountAlreadyPlaced(5))
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(5);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    if (GameDelegatesContainer.FuncCurrentSideDotCount() > 0)
                    {
                        return;
                    }
                    if (GameDelegatesContainer.FuncIsDotCountAlreadyPlaced(6))
                    {
                        return;
                    }
                    InputDelegatesContainer.SideSymbolPlacement(6);
                }
                return;
        }
    }
}