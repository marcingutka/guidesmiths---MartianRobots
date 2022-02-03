export const PaginationButton = (props: PaginationButtonProps) =>
{
    const { page, children, onClick } = props;

    return (
        <li className="page-item">
            <button className="page-link" onClick={() => onClick(page)}>{children? children : page}</button>
        </li>
    )
}

export interface PaginationButtonProps {
    page: number;
    onClick(page: number): any;
    children?: string
  }