import React from "react";
import { OrientationState } from "../components/Model/IGridAnalitics";

export const GridPointElement: React.FC<GridPointProps> = (props: GridPointProps) =>
{
  const { x, y, isDiscovered, isLost, noOfRobots, robotOrientation } = props;

  const baseClassName: string = "grid-point-size-column ";
  const styleClassName: string = ApplyStyle(isDiscovered, isLost);
  const finalClassName: string = baseClassName + styleClassName;
  const robotImgUrl: string = robotOrientation? ApplyRobotImg(isLost, robotOrientation) : "";

  return (
    <button className={finalClassName} style={{backgroundImage: 'url(/' + robotImgUrl + ")"}}></button>
  )
}

function ApplyStyle(isDiscovered: boolean, isLost: boolean): string
{
  const lost: string = "lost-grid-point "
  const discovered: string = "discovered-grid-point ";

  if (isLost) return lost;
  if (isDiscovered) return discovered;
  
  return "";
}

function ApplyRobotImg(isLost: boolean, robotOrientation: OrientationState): string
{
  const imgBasicUrl: string = "Robot";
  const lostUrl: string = isLost ? "lost" : "position";
  var orientation: string;

  switch (robotOrientation) {
    case OrientationState.North:
      orientation = "North";
      break;
    case OrientationState.East:
      orientation = "east";
      break;
    case OrientationState.South:
      orientation = "south";
      break;
    case OrientationState.West:
      orientation = "west";
      break;
      default:
        orientation = "";
  }

  const finalImgUrl = lostUrl + imgBasicUrl + orientation + ".png";

  return finalImgUrl;
}

export interface GridPointProps {
  x: number,
  y: number,
  isDiscovered: boolean,
  isLost: boolean,
  noOfRobots: number,
  robotOrientation?: OrientationState
} 