import { Position, OrientationState } from "./IPosition";

export interface IRobotStep {
    robotId: number,
    stepNumber: number,
    position: Position,
    orientation: OrientationState,
    command: string,
}