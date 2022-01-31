import { Position, OrientationState } from "./IPosition";

export interface IGridAnalitics {
    lostRobots: LostRobot[],
    discoveredArea: AreaAnalitics,
    gridPoints: GridPoint[],
    gridSize: Position
}

export interface GridPoint {
    coordinates: Position,
    robotsNumber: number
}

export interface AreaAnalitics {
    discoveredAreaAbsolute: number,
    discoveredAreaPercent: number,
    totalArea: number,
}

export interface LostRobot {
    robotId: number,
    position: Position,
    orientation: OrientationState
}