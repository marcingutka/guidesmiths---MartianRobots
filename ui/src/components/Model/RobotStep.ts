import { Position, OrientationState } from "./Position";

export interface RobotStep {
    robotId: number,
    stepNumber: number,
    position: Position,
    orientation: OrientationState,
    command?: RectangularMoveCommand,
    isLost: boolean,
    isLastStep: boolean
}

export enum RectangularMoveCommand {
    Left = 1,
    Right = 2,
    Forward = 3
}

export function getRectangularMoveCommandName(command: RectangularMoveCommand): string {
    switch (command)
    {        
        case RectangularMoveCommand.Left:
            return "Left";
        case RectangularMoveCommand.Right:
            return "Right";
        case RectangularMoveCommand.Forward:
            return "Forward";
        default:
            return "";
    }
}