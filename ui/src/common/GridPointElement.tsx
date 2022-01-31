import React from "react";
import { OrientationState } from "../components/Model/IPosition";
import { Tooltip, OverlayTrigger } from "react-bootstrap";

export const GridPointElement: React.FC<GridPointProps> = (props: GridPointProps) =>
{
  const { x, y, isDiscovered, isLost, noOfRobots, robotOrientation } = props;

  const baseClassName: string = "grid-point-size-column ";
  const styleClassName: string = ApplyStyle(isDiscovered, isLost);
  const finalClassName: string = baseClassName + styleClassName;
  const robotImgUrl: string = robotOrientation? ApplyRobotImg(isLost, robotOrientation) : "";

  return (
    <span className="grid-point-size-column">
      { noOfRobots && noOfRobots > 0 ? <OverlayTrigger key="top" placement={"top"} overlay={
        <Tooltip id={'tooltip-top'}>
          Number of robots: {noOfRobots}
        </Tooltip>}>
        <button className={finalClassName} style={{backgroundImage: 'url(/' + robotImgUrl + ")"}}/>
      </OverlayTrigger> : <button className={finalClassName} style={{backgroundImage: 'url(/' + robotImgUrl + ")"}}/>}
    </span>
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
      orientation = "East";
      break;
    case OrientationState.South:
      orientation = "South";
      break;
    case OrientationState.West:
      orientation = "West";
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
  noOfRobots?: number,
  robotOrientation?: OrientationState
} 