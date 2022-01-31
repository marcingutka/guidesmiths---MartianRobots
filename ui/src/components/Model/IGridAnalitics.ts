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