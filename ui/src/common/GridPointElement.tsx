import React from "react";

export const GridPointElement: React.FC<GridPointProps> = (props: GridPointProps) =>
{
  const { x, y, isDiscovered, isLost, noOfRobots } = props;

  const baseClassName: string = "grid-point-size-column ";
  const styleClassName: string = ApplyStyle(isDiscovered, isLost);
  const finalClassName: string = baseClassName + styleClassName;

  console.log(finalClassName, isDiscovered);

  return (
    <button className={finalClassName}></button>
  )
}

function ApplyStyle(isDiscovered: boolean, isLost: boolean): string
{
  var lost: string = "lost-grid-point "
  var discovered: string = "discovered-grid-point ";

  if (isLost) return lost;
  if (isDiscovered) return discovered;
  
  return "";
}

export interface GridPointProps {
  x: number,
  y: number,
  isDiscovered: boolean,
  isLost: boolean,
  noOfRobots: number
} 