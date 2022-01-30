import React from "react";

export const GridPointElement: React.FC<GridPointProps> = (props: GridPointProps) =>
{
  const { x, y, isLost, noOfRobots } = props;

  const baseClassName: string = "grid-point-size-column ";
  const customClassName: string = "lost-grid-point"
  const finalClassName: string = baseClassName + (isLost ? customClassName : "");

  return (
    <button className={finalClassName}></button>
  )
}

export interface GridPointProps {
  x: number,
  y: number,
  isLost: boolean,
  noOfRobots: number
} 