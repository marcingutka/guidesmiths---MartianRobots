import { Col, Row } from "react-bootstrap";

export const StatisticRow = (props: IStatisticRow) =>
{
  const { name, value } = props;

  return (
    <Row className="align-items-center justify-content-md-start">
      <Col className="justify-content-start col-10">{name}</Col>
      <Col className="justify-content-end col-2">{value}</Col>
    </Row>
  )
}

export interface IStatisticRow {
  name: string,
  value: string | number
}