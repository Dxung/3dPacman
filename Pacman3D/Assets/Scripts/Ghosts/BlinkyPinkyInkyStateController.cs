using UnityEngine;

public class BlinkyPinkyInkyStateController : GhostStateController
{

    protected override void UpdateGhostState()
    {
        if (CheckCurrentState(GhostState.scatter))
        {
            UpdateScatterMode();
        }

        //after iteration go to 4, ghost will be in chase for the rest of the game (not counting frightened/eaten)
        else if (CheckCurrentState(GhostState.chase))
        {
            if (_modeSwitchIteration == 1 || _modeSwitchIteration == 2 || _modeSwitchIteration == 3)
            {
                UpdateChaseMode();
            }
        }
        else if (CheckCurrentState(GhostState.frightened))
        {
            UpdateFrightenedMode();
        }
    }
}

