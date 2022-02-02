import React from "react";

export const Pagination = (props: IPaginationProps) =>
{
  const {page, pages, onClick} = props;

  let buttons:JSX.Element;
  
  if (pages === 1) return null;
  else if (pages <= 3 || page === 1)
  {
    buttons = 
    <React.Fragment>
      <li className="page-item ">
        <button className="page-link" onClick={() => onClick(1)}>1</button>
      </li>
      <li className="page-item">
        <button className="page-link" onClick={() => onClick(2)}>2</button>
      </li>
      {pages >= 3 && <li className="page-item">
        <button className="page-link" onClick={() => onClick(3)}>3</button>
      </li>}
    </React.Fragment>
  }
  else if (page === pages)
  {
    buttons = 
    <React.Fragment>
      <li className="page-item ">
        <button className="page-link" onClick={() => onClick(page - 2)}>{ page - 2 }</button>
      </li>
      <li className="page-item">
        <button className="page-link" onClick={() => onClick(page - 1)}>{ page - 1 }</button>
      </li>
      {<li className="page-item">
        <button className="page-link" onClick={() => onClick(page)}>{ page }</button>
      </li>}
    </React.Fragment>
  }
  else
  {
    buttons = 
    <React.Fragment>
      <li className="page-item ">
        <button className="page-link" onClick={() => onClick(page - 1)}>{ page - 1 }</button>
      </li>
      <li className="page-item">
        <button className="page-link" onClick={() => onClick(page)}>{ page }</button>
      </li>
      {<li className="page-item">
        <button className="page-link" onClick={() => onClick(page + 1)}>{ page + 1 }</button>
      </li>}
    </React.Fragment>
  }

  return (
    <React.Fragment>
      <nav aria-label="...">
        <ul className="pagination justify-content-center">
          <li className="page-item">
            <button className="page-link" onClick={() => onClick(1)}>First</button> 
          </li>
          {buttons}
          <li className="page-item">
            <button className="page-link" onClick={() => onClick(pages)}>Last</button>  
          </li>
        </ul>
      </nav>
    </React.Fragment>
  )
}

export interface IPaginationProps {
  pages: number;
  page: number;
  onClick(page: number): any; 
}