import React from "react";

export const PaginationButton = (props: PaginationButtonProps) =>
{
    const { page, onClick } = props;

    return (
        <li className="page-item">
            <button className="page-link" onClick={() => onClick(page)}>{page}</button>
        </li>
    )
}


export interface PaginationButtonProps {
    page: number;
    onClick(page: number): any; 
  }