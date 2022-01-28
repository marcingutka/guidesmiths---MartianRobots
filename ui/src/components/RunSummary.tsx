import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { IDataSet } from "./Model/IDataSet";
import { GetDataSets } from '../services/DataSetApiRequest';

import { Pagination } from './utils/Pagination';

export const RunSummary = () =>
{
  const displayedItem: number = 10;
  const [page, setPage] = React.useState(1);
  const [pages, setPages] = React.useState(1);
  const [data, setData] = React.useState<IDataSet[]>([]);
  const {id} = useParams();

  React.useEffect(() => {
    const fetchData = async () => {
      const response = await GetDataSets();
      setData(response.data)
    }
      fetchData();
  }, []);

  React.useEffect(() => {
    setPages(Math.ceil(data.length/displayedItem));
  }, [data, displayedItem])

  const isPaginated: boolean = data.length > displayedItem;
  
  var paginatedData: IDataSet[];

  paginatedData = isPaginated? Paginate(data, page, displayedItem) : data;


   return (
    <React.Fragment>
      <Container>
        <Row>
          <Col>Upload File</Col>
          <Col>
            {id}
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  )
}

function Paginate(data: IDataSet[], page: number, elementNumber: number): IDataSet[] {
  const startIndex = (page - 1) * elementNumber;
  const endIndex = data.length > startIndex + elementNumber ? startIndex + elementNumber : data.length;
  return data.slice(startIndex, endIndex);
}

function onClickDataNameHandler(runId: string)
{
  console.log("runId", runId);
}