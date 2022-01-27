import React from "react";
import { Col, Container, Row } from "react-bootstrap";
import axios from 'axios';

export const Main = () =>
{
  const [data, setData] = React.useState<IGetResult[]>();

  React.useEffect(() => {
    axios.get('https://localhost:7236/api/dataSet').then((response) => setData(response.data)).catch(() => console.log('failed'));
  }, []);

    return (
      <Container>
        <Row>
          {data?.map( x=> <Col>{x.name}</Col>)}
          <Col>MArincc</Col>
        </Row>
      </Container>
    )
}

export interface IGetResult {
  runId: string;
  name: string;
  generationDate: string;
}