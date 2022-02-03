import React from "react";
import { PaginationButton } from "./PaginationButton";

export const Pagination = (props: PaginationProps) =>
{
  const {page, pages, onClick} = props;

  let buttons:JSX.Element;
  
  if (pages === 1) return null;
  else if (pages <= 3 || page === 1)
  {
    buttons = 
    <React.Fragment>
      <PaginationButton page={1} onClick={onClick} />
      <PaginationButton page={2} onClick={onClick} />
      {pages >= 3 && <PaginationButton page={3} onClick={onClick} />}
    </React.Fragment>
  }
  else if (page === pages)
  {
    buttons = 
    <React.Fragment>
      <PaginationButton page={page - 2} onClick={onClick} />
      <PaginationButton page={page - 1} onClick={onClick} />
      <PaginationButton page={page} onClick={onClick} />
    </React.Fragment>
  }
  else
  {
    buttons = 
    <React.Fragment>
      <PaginationButton page={page - 1} onClick={onClick} />
      <PaginationButton page={page} onClick={onClick} />
      <PaginationButton page={page + 1} onClick={onClick} />
    </React.Fragment>
  }

  return (
    <React.Fragment>
      <nav aria-label="...">
        <ul className="pagination justify-content-center">
          <PaginationButton page={1} onClick={onClick}>First</PaginationButton>
          {buttons}
          <PaginationButton page={pages} onClick={onClick}>Last</PaginationButton>
        </ul>
      </nav>
    </React.Fragment>
  )
}

export interface PaginationProps {
  pages: number;
  page: number;
  onClick(page: number): any; 
}