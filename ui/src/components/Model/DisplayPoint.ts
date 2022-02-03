import { Position, OrientationState } from "./Position";

export class DisplayPoint {
    coordinates: Position;
    orientation?: OrientationState;
    isDiscovered: boolean;
    isLost: boolean;
    robotsNumber?: number;

    constructor(coordinates: Position, isDiscovered: boolean, isLost: boolean, robotsNumber?: number, orientation?: OrientationState)
    {
        this.coordinates = coordinates;
        this.isDiscovered = isDiscovered;
        this.isLost = isLost;
        this.orientation = orientation;
        this.robotsNumber = robotsNumber;
    }
}