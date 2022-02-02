export enum OrientationState {
    North = 1,
    East = 2,
    South = 3,
    West = 4,
}

export interface Position {
    x: number,
    y: number,
}

export function getOrientationStateName(command: OrientationState): string {
    switch (command)
    {        
        case OrientationState.North:
            return "North";
        case OrientationState.East:
            return "East";
        case OrientationState.South:
            return "South";
        case OrientationState.West:
            return "West";
        default:
            return "";
    }
}